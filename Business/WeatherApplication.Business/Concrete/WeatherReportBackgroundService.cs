using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherApplication.DataAccess.Context;
using WeatherApplication.Entities.Concrete.TableModels;
using WeatherApplication.Entities.Concrete.TableModels.Models;

namespace WeatherApplication.Business.Concrete
{
    public class WeatherReportBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<WeatherReportServiceDal> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public WeatherReportBackgroundService(IServiceScopeFactory scopeFactory, ILogger<WeatherReportServiceDal> logger, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _configuration = configuration;
            // Timeout parametrləri ilə HttpClient-i konfiqurasiya edin
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(3) // Əgər tələb 3 saat ərzində tamamlanmazsa, dayandırır
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 10 saniyədən bir işləyən döngü
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    // DbContext-ə müraciət edək
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    // Bütün distriktləri alaq
                    var districts = await context.Districts.ToListAsync(stoppingToken);

                    foreach (var district in districts)
                    {
                        var weatherData = await FetchWeatherDataAsync(district.Latitude, district.Longitude, district.Id);
                        if (weatherData != null)
                        {
                            var existingReport = await context.WeatherReports
                                .FirstOrDefaultAsync(w => w.DistrictId == district.Id && w.WeatherId == weatherData.WeatherId, stoppingToken);
                            if (existingReport != null)
                            {
                                // Update the existing report
                                existingReport.Temp = weatherData.Temp;
                                existingReport.FeelsLike = weatherData.FeelsLike;
                                existingReport.TempMin = weatherData.TempMin;
                                existingReport.TempMax = weatherData.TempMax;
                                existingReport.Pressure = weatherData.Pressure;
                                existingReport.Humidity = weatherData.Humidity;
                                existingReport.SeaLevel = weatherData.SeaLevel;
                                existingReport.GroundLevel = weatherData.GroundLevel;
                                existingReport.WindSpeed = weatherData.WindSpeed;
                                existingReport.WindDegree = weatherData.WindDegree;
                                existingReport.WindGust = weatherData.WindGust;
                                existingReport.Clouds = weatherData.Clouds;
                                existingReport.DateTime = DateTime.UtcNow.AddHours(4);
                                existingReport.UpdateDate = DateTime.UtcNow.AddHours(4);

                                await context.SaveChangesAsync(stoppingToken);
                                _logger.LogInformation($"Weather data for {district.Title} updated in database.");
                            }
                            else
                            {
                                // Add new weather report
                                context.WeatherReports.Add(weatherData);
                                await context.SaveChangesAsync(stoppingToken);
                                _logger.LogInformation($"Weather data for {district.Title} added to database.");
                            }
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken); // 3 saat gecikmə
            }
        }

        // Hava məlumatlarını API-dən alaq
        private async Task<WeatherReport> FetchWeatherDataAsync(double lat, double lon, int districtId)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var requestUri = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);

                // Uğurlu cavab yoxlaması
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(json);

                    if (weatherData != null)
                    {
                        return MapToWeatherReport(weatherData, districtId);
                    }
                }
                else
                {
                    _logger.LogWarning($"Failed to fetch weather data: {response.ReasonPhrase} ({response.StatusCode})");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP error while fetching weather data: {httpEx.Message}");
            }
            catch (TaskCanceledException taskEx)
            {
                _logger.LogError($"Request timeout: {taskEx.Message}");
            }

            return null;
        }

        // Hava məlumatlarını `WeatherReport` modelinə çevirək
        private WeatherReport MapToWeatherReport(WeatherApiResponse apiResponse, int districtId)
        {
            var firstWeather = apiResponse.Weather?.FirstOrDefault();
            if (firstWeather == null)
            {
                _logger.LogWarning("Received empty weather data.");
                return null;
            }

            return new WeatherReport
            {
                WeatherId = firstWeather.Id,
                Main = firstWeather.Main,
                Description = firstWeather.Description,
                Icon = firstWeather.Icon,
                Temp = apiResponse.Main.Temp,
                FeelsLike = apiResponse.Main.FeelsLike,
                TempMin = apiResponse.Main.TempMin,
                TempMax = apiResponse.Main.TempMax,
                Pressure = apiResponse.Main.Pressure,
                Humidity = apiResponse.Main.Humidity,
                SeaLevel = apiResponse.Main.SeaLevel ?? 0,
                GroundLevel = apiResponse.Main.GrndLevel ?? 0,
                WindSpeed = apiResponse.Wind.Speed,
                WindDegree = apiResponse.Wind.Deg,
                WindGust = apiResponse.Wind.Gust ?? 0,
                Clouds = apiResponse.Clouds.All,
                DateTime = DateTime.UtcNow,
                DistrictId = districtId
            };
        }

    }
}

