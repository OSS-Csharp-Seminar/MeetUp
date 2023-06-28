using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Models
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [ForeignKey("MeetActivity")]
        public int ActivityId { get; set; }
        public MeetActivity Activity { get; set; }
        public bool Approved { get; set; }

        public UserActivity() {
        }

        public UserActivity(string userId, int activityId, bool approved=false)
        {
            UserId = userId;
            ActivityId = activityId;
            Approved = approved;
        }
        
    }
}
