using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.ViewModels;


namespace MeetUp.Services
{
    public class MeetActivityService :IMeetActivityService
    {
        private readonly IMeetActivityRepository repo;
        private readonly IUserActivityService userActivityService;
        private readonly IUserService userService;
        private readonly ILocationService locationService;
        private readonly ICityService cityService;
        public MeetActivityService(IMeetActivityRepository meetActivityRepository, IUserActivityService userActivityService, IUserService userService, ILocationService locationService)
        {
            repo = meetActivityRepository;
            this.userActivityService = userActivityService;
            this.userService = userService;
            this.locationService = locationService;
        }

        public bool Add(MeetActivityCreateModel meetActivity, string userId)
        {
            meetActivity.AppUserId = userId;
            var location = locationService.Add(new Location(meetActivity.Address, meetActivity.CityId));
            var created = repo.Add(MeetActivityCreateModel.To(meetActivity, location.Id));
            if (created != null)
            {
                userActivityService.Add(userId, created.Id);
                return true;
            }
            return false;
        }

        public bool Delete(Models.MeetActivity meetActivity)
        {
            return repo.Delete(meetActivity);
        }

        public Task<ICollection<Models.MeetActivity>> GetAll()
        {
            return repo.GetAll();
        }

        public Task<Models.MeetActivity> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(Models.MeetActivity meetActivity)
        {
            return repo.Update(meetActivity);   
        }

        public String Validate(MeetActivityCreateModel meetActivity)
        {
            if (meetActivity.Time <= DateTime.Now)
            {
                return "Date cannot be in the past.";
            }

            return "";
        }

        public  bool canJoin(int activityId, string userId, bool isAuthenticated)
        {
            var members = userActivityService.GetUsersByActivityId(activityId).Result;
            var user = userService.GetById(userId).Result;
            if (!members.Contains(user)
                && GetById(activityId).Result.Capacity > members.Count
                && (isAuthenticated))
            {
                return true;
            }

            return false;
        }
    }
}
