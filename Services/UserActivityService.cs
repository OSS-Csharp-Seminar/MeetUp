using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.VisualBasic;

namespace MeetUp.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository repo;
        private readonly IUserService userService;
        
        public UserActivityService(IUserActivityRepository userActivityRepository, IUserService userService)
        {
            repo = userActivityRepository;
            this.userService = userService;
        }

        public Task<ICollection<AppUser>> ApprovedUsers(int activityId)
        {
            return repo.ApprovedUsers(activityId);
        }

        public bool isSubscribed(int activityId, string userId)
        {
            var userActivity = repo.GetByUserAndActivity(userId, activityId).Result;
            if (userActivity != null)
            {
                return true;
            }

            return false;
        }

        public async Task<ICollection<UserActivity>> GetByActivityOwner(string userId)
        {
            return await repo.GetByActivityOwner(userId);
            
        }

        public void Approve(string userId, int activityId)
        {
            var userActivity = repo.GetByUserAndActivity(userId, activityId).Result;
            userActivity.Approved = true;
            repo.Update(userActivity);
        }

        public bool Add(UserActivity userActivity)
        {
            return repo.Add(userActivity);
        }

        public bool Delete(UserActivity userActivity)
        {
            return repo.Delete(userActivity);
        }

        public Task<ICollection<UserActivity>> GetAll()
        {
           return repo.GetAll();
        }

        public Task<UserActivity> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(UserActivity userActivity)
        {
           return repo.Update(userActivity);
        }
    }
}
