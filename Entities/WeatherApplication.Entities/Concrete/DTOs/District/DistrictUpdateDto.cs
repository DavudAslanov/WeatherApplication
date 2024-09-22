using System;
using WeatherApplication.Entities.Concrete.TableModels;

namespace WeatherApplication.Entities.Concrete.DTOs;

public class DistrictUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public static DistrictUpdateDto ToDistrict(District district)
    {
        DistrictUpdateDto districtDto = new ()
        {
            Id = district.Id,
            Title = district.Title,
            Latitude = district.Latitude,
            Longitude = district.Longitude
        };
        return districtDto;
    }
    public static District ToDistrict(DistrictUpdateDto districtDto)
    {
        District discrit = new()
        {
            Id = districtDto.Id,
            Title = districtDto.Title,
            Latitude = districtDto.Latitude,
            Longitude = districtDto.Longitude
        };
        return discrit;
    }
}