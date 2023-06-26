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

        public Task<ICollection<AppUser>> GetUsersByActivityId(int activityId)
        {
            return repo.GetUsersByActivityId(activityId);
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

        public bool Add(string userId, int activityId)
        {
            var userActivity = new UserActivity();
            userActivity.UserId = userId;
            userActivity.ActivityId = activityId;
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
