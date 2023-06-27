using MeetUp.Models;

namespace MeetUp.ViewModels
{
    public class UserDetailsViewModel
    {
        public AppUser User{ get; set; }
        public ICollection<Rating> Ratings { get; set; }

        public ICollection<UserActivity> UserActivities { get; set; }
        public double AverageScore { get; set; }

        public UserDetailsViewModel(ICollection<Rating> ratings, ICollection<UserActivity> userActivities, AppUser user)
        {
            Ratings = ratings;
            UserActivities = userActivities;
            User = user;
        }
        public void CalculateAverageScore()
        {
            double sum = 0;
            foreach (Rating rating in Ratings)
            {
                sum = sum + rating.Score;
            }
           
            AverageScore =  sum / Ratings.Count;

        }

    }
}
