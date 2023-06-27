using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public City(string name)
        {
            Name = name;
        }
        
        public City()
        {
        }
    }
}
