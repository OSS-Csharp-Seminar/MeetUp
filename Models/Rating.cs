using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Range(1,5)]
        public int Score { get; set; }
        public string Message { get; set; }
        [ForeignKey("User")]
        public string RevieweeId { get; set; }
        public AppUser? Reviewee { get; set; }
    }   
}
