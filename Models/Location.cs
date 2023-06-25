using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string City { get; set; }
        
        public Location(decimal longitude, decimal latitude, string city)
        {
            Longitude = longitude;
            Latitude = latitude;
            City = city;
        }
    }
    
    
}
