using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.AspNet.Identity;

namespace MeetUp.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository repo;
        private readonly IMeetActivityService meetActivityService;
        
        public UserActivityService(IUserActivityRepository userActivityRepository, IMeetActivityService meetActivityService)
        {
            repo = userActivityRepository;
            this.meetActivityService = meetActivityService;
        }

        public Task<ICollection<AppUser>> GetUsersByActivityId(int activityId)
        {
            return repo.GetUsersByActivityId(activityId);
        }

        public bool Add(UserActivity userActivity)
        {
            //TODO: This should throw exception instead of bool, when FE can handle it
            if (validate(userActivity))
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

        private bool validate(UserActivity userActivity)
        {
            var activity = meetActivityService.GetById(userActivity.ActivityId).Result;
            if (GetAll().Result.Count >= activity.Capacity)
            {
                return false;
            }

            return true;
        }
    }
}
