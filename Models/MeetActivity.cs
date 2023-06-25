using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeetUp.ViewModels;

namespace MeetUp.Models
{
    public class MeetActivity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int Capacity { get; set; }
        public byte[]? Picture { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("Owner")]
        public string AppUserId { get; set; }
        public AppUser Owner;
        
        public MeetActivity(string name, string description, DateTime time, int capacity, byte[]? picture, int locationId, int categoryId)
        {
            Name = name;
            Description = description;
            Time = time;
            Capacity = capacity;
            Picture = picture;
            LocationId = locationId;
            CategoryId = categoryId;
        }
        
    }
}
