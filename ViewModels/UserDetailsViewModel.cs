using MeetUp.Models;

namespace MeetUp.ViewModels
{
    public class UserDetailsViewModel
    {
        public AppUser User{ get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public double ReviewScore { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }
        public UserDetailsViewModel(ICollection<Rating> ratings, ICollection<UserActivity> userActivities, AppUser user)
        {
            Ratings = ratings;
            UserActivities = userActivities;
            User = user;

            double sum = 0;
            foreach (Rating rating in ratings)
            {
                sum += rating.Score;
            }


            ReviewScore = sum/Ratings.Count;

        }

    }
}
