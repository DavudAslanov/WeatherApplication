using System;
using WeatherApplication.Core.Entities.Concrete;

namespace WeatherApplication.Entities.Concrete.TableModels;

public class District:BaseEntity
{
    public District()
    {
        WeatherReports = new List<WeatherReport>();
    }
    public string Title { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<WeatherReport> WeatherReports { get; set; } 
}

