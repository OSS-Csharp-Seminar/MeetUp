using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
