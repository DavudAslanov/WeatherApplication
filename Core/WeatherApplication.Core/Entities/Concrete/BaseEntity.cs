using System;
namespace WeatherApplication.Core.Entities.Concrete;
    public class BaseEntity
    {
        public int Id { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }


