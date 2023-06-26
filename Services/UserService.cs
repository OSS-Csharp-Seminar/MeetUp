using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.ViewModels;

namespace MeetUp.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository repo;
        public readonly IRatingRepository ratingRepository;
        public readonly IUserActivityRepository activityRepository;
        public UserService(IUserRepository userRepository, IRatingRepository ratingRepository, IUserActivityRepository activityRepository)
        {
            repo = userRepository;
            this.ratingRepository = ratingRepository;
            this.activityRepository = activityRepository;
        }
        public bool Add(AppUser user)
        {
           return repo.Add(user);
        }

        public bool Delete(AppUser user)
        {
           return repo.Delete(user);
        }

        public Task<ICollection<AppUser>> GetAll()
        {
           return repo.GetAll();
        }

        public Task<AppUser> GetById(string id)
        {
            return repo.GetById(id);
        }

        public async Task<UserDetailsViewModel> GetUserDetails(string id)
        {
            AppUser user = await repo.GetById(id);
            ICollection<Rating> ratings = await ratingRepository.GetRatingByUserId(id);
            ICollection<UserActivity> userActivities = await activityRepository.GetActivitiesByUserId(id);
            UserDetailsViewModel viewModel = new UserDetailsViewModel(ratings, userActivities, user);

            viewModel.CalculateAverageScore();

            return viewModel;
        }

        public bool Update(AppUser user)
        {
           return repo.Update(user);
        }
    }
}
