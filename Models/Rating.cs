using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public string Message { get; set; }
        [ForeignKey("User")]
        public int RevieweeId { get; set; }
        public User Reviewee { get; set; }
    }   
}
