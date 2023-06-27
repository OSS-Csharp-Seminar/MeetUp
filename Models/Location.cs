using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
        
        public Location(string address, int cityId)
        {
            Address = address;
            CityId = cityId;
        }
        public Location()
        {
            
        }
    }
    
    
}
