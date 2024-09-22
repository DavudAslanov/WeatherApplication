using System;
using WeatherApplication.Entities.Concrete.TableModels;
namespace WeatherApplication.Entities.Concrete.DTOs;
public class DistrictDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public static List<DistrictDto> ToDistrictDto(District district)
    {
        DistrictDto districtDto = new DistrictDto
        {
            Id = district.Id,
            Title = district.Title,
            Latitude = district.Latitude,
            Longitude = district.Longitude
        };
        List<DistrictDto> districtDtoList = new List<DistrictDto>();
        districtDtoList.Add(districtDto);
        return districtDtoList;
    }
    public static District ToDiscritDtos(DistrictDto districtDto)
    {
        District district = new District
        {
            Title = districtDto.Title,
            Latitude = districtDto.Latitude,
            Longitude = districtDto.Longitude
        };
        return district;
    }
}