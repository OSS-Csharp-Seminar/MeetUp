using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.VisualBasic;

namespace MeetUp.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository repo;
        private readonly IUserService userService;
        private readonly IMeetActivityService meetActivityService;
        
        public UserActivityService(IUserActivityRepository userActivityRepository, IMeetActivityService meetActivityService, IUserService userService)
        {
            repo = userActivityRepository;
            this.meetActivityService = meetActivityService;
            this.userService = userService;
        }

        public Task<ICollection<AppUser>> GetUsersByActivityId(int activityId)
        {
            return repo.GetUsersByActivityId(activityId);
        }

        public async Task<ICollection<UserActivity>> GetByActivityOwner(string userId)
        {
            var userActivities = repo.GetAll().Result;
            foreach (UserActivity userActivity in userActivities)
            {
                if (userActivity.Activity.AppUserId != userId)
                {
                    userActivities.Remove(userActivity);
                }
            }

            return userActivities;
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
            if (!GetUsersByActivityId(activityId).Result.Contains(userService.GetById(userId).Result))
            {
                return repo.Add(userActivity);
            }

            return false;
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
