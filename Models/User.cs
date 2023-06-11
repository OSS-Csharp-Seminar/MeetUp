using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public enum Role { ADMIN, USER }
        public ICollection<MeetActivity> MyActivities{ get; set; }
        public ICollection<UserActivity> MyUserActivities{ get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
