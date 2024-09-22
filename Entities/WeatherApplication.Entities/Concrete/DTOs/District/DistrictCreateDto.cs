using System;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Entities.Concrete.DTOs;

public class DistrictCreateDto
{
    public string Title { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public static District ToDistrcit(DistrictCreateDto createDto)
    {
        District district = new()
        {
            Title = createDto.Title,
            Latitude = createDto.Latitude,
            Longitude = createDto.Longitude
        };
        return district;
    }
}

