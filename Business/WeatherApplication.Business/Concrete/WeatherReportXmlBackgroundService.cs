using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Serialization;
using WeatherApplication.DataAccess.Context;
using WeatherApplication.Entities.Concrete.TableModels;
using WeatherApplication.Entities.Concrete.TableModels.Models;
using WeatherApplication.Entities.Concrete.TableModels.Models.XMLModels;
using WeatherApplication.Entities.Concrete.TableModels.ModelXml; // WeatherReportXml sınıfı için

namespace WeatherApplication.Business.Concrete
{
    public class WeatherReportXmlBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<WeatherReportXmlBackgroundService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WeatherReportXmlBackgroundService(IServiceScopeFactory scopeFactory, ILogger<WeatherReportXmlBackgroundService> logger, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var districts = await context.Districts.ToListAsync(stoppingToken);

                    foreach (var district in districts)
                    {
                        try
                        {


                            var weatherData = await FetchWeatherDataAsync(district.Latitude, district.Longitude, district.Id);
                            if (weatherData != null)
                            {
                                // Var olan veriyi kontrol et
                                var existingReport = await context.WeatherReportXmls
                                    .FirstOrDefaultAsync(w => w.DistrictId == district.Id && w.WeatherId == weatherData.WeatherId, stoppingToken);

                                if (existingReport != null)
                                {
                                    // Var olan veriyi güncelle
                                    existingReport.XmlData = SerializeToXml(weatherData);
                                    existingReport.DateTime = DateTime.UtcNow.AddHours(4);
                                    existingReport.UpdateDate = DateTime.UtcNow.AddHours(4);

                                    await context.SaveChangesAsync(stoppingToken);
                                    _logger.LogInformation($"Weather data for {district.Title} updated in database.");
                                }
                                else
                                {

                                    context.WeatherReportXmls.Add(new WeatherReportXml
                                    {
                                        DistrictId = district.Id,
                                        WeatherId = weatherData.WeatherId,
                                        XmlData = SerializeToXml(weatherData),
                                        DateTime = DateTime.UtcNow.AddHours(4)
                                    });
                                    await context.SaveChangesAsync(stoppingToken);
                                    _logger.LogInformation($"Weather data for {district.Title} added to database.");
                                }
                            }
                            else
                            {
                                _logger.LogInformation($"No weather data found for {district.Title}.");
                            }
                        }
                        catch(Exception ex )
                        {
                            _logger.LogError($"Error processing weather data for {district.Title}: {ex.Message}");
                        }
                    }
                    await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
                }
            }

            WeatherReportXml MapToWeatherReportXml(WeatherApiResponse apiResponse, int districtId)
            {
                var firstWeather = apiResponse.Weather?.FirstOrDefault();
                if (firstWeather == null)
                {
                    _logger.LogWarning("Received empty weather data.");
                    return null;
                }

                return new WeatherReportXml
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
                    DistrictId = districtId,
                    IsDeleted = 0,
                    UpdateDate = null
                };
            }

            async Task<WeatherReportXml> FetchWeatherDataAsync(double lat, double lon, int districtId)
            {
                var apiKey = _configuration["WeatherApi:ApiKey"];
                var requestUri = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

                try
                {
                    var response = await _httpClient.GetAsync(requestUri);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        // JSON verisini deserialize et
                        var apiResponse = JsonConvert.DeserializeObject<WeatherApiResponse>(json);
                        return MapToWeatherReportXml(apiResponse, districtId);
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
            string SerializeToXml(WeatherReportXml weatherReportXml)
            {
                var serializer = new XmlSerializer(typeof(WeatherReportXml));
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, weatherReportXml);
                    return stringWriter.ToString();
                }
            }
        }
    }
}
