using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Location Location { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
