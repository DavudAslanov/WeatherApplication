using System;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Entities.Concrete.DTOs;

public class WeatherReportUpdateDto
{
    public int Id { get; set; }
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

    public static WeatherReportUpdateDto ToWeatherReport(WeatherReport weatherReport)
    {
        WeatherReportUpdateDto weatherReportDto = new()
        {
            Id = weatherReport.Id,
            WeatherId = weatherReport.WeatherId,
            Main = weatherReport.Main,
            Description = weatherReport.Description,
            Icon = weatherReport.Icon,
            Temp = weatherReport.Temp,
            FeelsLike = weatherReport.FeelsLike,
            TempMin = weatherReport.TempMin,
            TempMax = weatherReport.TempMax,
            Pressure = weatherReport.Pressure,
            Humidity = weatherReport.Humidity,
            SeaLevel = weatherReport.SeaLevel,
            GroundLevel = weatherReport.GroundLevel,
            WindDegree = weatherReport.WindDegree,
            WindGust = weatherReport.WindGust,
            Clouds = weatherReport.Clouds,
            DateTime = weatherReport.DateTime,
            DistrictId = weatherReport.DistrictId,
            WindSpeed = weatherReport.WindSpeed
        };
        return weatherReportDto;
    }
    public static WeatherReport ToWeatherReport(WeatherReportUpdateDto weatherReportDto)
    {
        WeatherReport report = new()
        {
            Id = weatherReportDto.Id,
            WeatherId = weatherReportDto.WeatherId,
            Main = weatherReportDto.Main,
            Description = weatherReportDto.Description,
            Icon = weatherReportDto.Icon,
            Temp = weatherReportDto.Temp,
            FeelsLike = weatherReportDto.FeelsLike,
            TempMin = weatherReportDto.TempMin,
            TempMax = weatherReportDto.TempMax,
            Pressure = weatherReportDto.Pressure,
            Humidity = weatherReportDto.Humidity,
            SeaLevel = weatherReportDto.SeaLevel,
            GroundLevel = weatherReportDto.GroundLevel,
            WindDegree = weatherReportDto.WindDegree,
            WindGust = weatherReportDto.WindGust,
            Clouds = weatherReportDto.Clouds,
            DateTime = weatherReportDto.DateTime,
            DistrictId = weatherReportDto.DistrictId,
            WindSpeed = weatherReportDto.WindSpeed
        };
        return report;
    }
}

