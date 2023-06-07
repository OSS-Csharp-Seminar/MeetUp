using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Models
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("MeetActivity")]
        public int ActivityId { get; set; }
        public MeetActivity Activity { get; set; }
    }
}
