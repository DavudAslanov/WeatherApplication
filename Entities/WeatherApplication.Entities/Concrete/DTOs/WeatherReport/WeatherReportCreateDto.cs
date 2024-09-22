using System;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Entities.Concrete.DTOs;
public class WeatherReportCreateDto
{
    public int WeatherId { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public float Temp { get; set; }
    public float FeelsLike { get; set; }
    public float TempMin { get; set; }
    public float TempMax { get; set; }
    public float Pressure { get; set; }
    public float Humidity { get; set; }
    public float SeaLevel { get; set; }
    public float GroundLevel { get; set; }
    public float WindSpeed { get; set; }
    public float WindDegree { get; set; }
    public float WindGust { get; set; }
    public float Clouds { get; set; }
    public DateTime DateTime { get; set; }
    public int DistrictId { get; set; }

    public static WeatherReport ToWeatherReport(WeatherReportCreateDto createDto)
    {
        WeatherReport weatherReport = new()
        {
            WeatherId = createDto.WeatherId,
            Main = createDto.Main,
            Description = createDto.Description,
            Icon = createDto.Icon,
            Temp = createDto.Temp,
            FeelsLike = createDto.FeelsLike,
            TempMin = createDto.TempMin,
            TempMax = createDto.TempMax,
            Pressure = createDto.Pressure,
            Humidity = createDto.Humidity,
            SeaLevel = createDto.SeaLevel,
            GroundLevel = createDto.GroundLevel,
            WindDegree = createDto.WindDegree,
            WindGust = createDto.WindGust,
            Clouds = createDto.Clouds,
            DateTime = createDto.DateTime,
            DistrictId = createDto.DistrictId,
            WindSpeed = createDto.WindSpeed
        };
        return weatherReport;
    }
}

