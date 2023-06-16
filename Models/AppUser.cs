using MeetUp.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MeetUp.Models
{

    public class AppUser : IdentityUser
    {
        public ICollection<MeetActivity>? MyActivities{ get; set; }
        public ICollection<UserActivity>? MyUserActivities{ get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}
