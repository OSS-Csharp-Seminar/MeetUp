using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.ViewModels;
using Org.BouncyCastle.Asn1.Cms;


namespace MeetUp.Services
{
    public class MeetActivityService : IMeetActivityService
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
                userActivityService.Add(new UserActivity(userId, meetActivity.Id, true));
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

        public bool Update(MeetActivityEditModel meetActivity)
        {
            //TODO: Doesn't have to create each time
            var location = locationService.GetByAddressAndCityId(meetActivity.Address, meetActivity.CityId).Result;
            if (location == null)
            {
                location = locationService.Add(new Location(meetActivity.Address, meetActivity.CityId));
            }

            return repo.Update(MeetActivityEditModel.To(meetActivity, location.Id));   
        }

        public String ValidateDate(DateTime activityTime)
        {
            if (activityTime <= DateTime.Now)
            {
                return "Date cannot be in the past.";
            }

            return "";
        }

        public canJoin canJoin(int activityId, string userId, bool isAuthenticated)
        {
            var approvedMembers = userActivityService.ApprovedUsers(activityId).Result;
            var user = userService.GetById(userId).Result;
            if (!isAuthenticated)
            {
                return Models.canJoin.FALSE;
            }
            if (approvedMembers.Contains(user))
            {
                return Models.canJoin.APPROVED;
            }
            if(userActivityService.isSubscribed(activityId, userId))
            {
                return Models.canJoin.PENDING;
            }
            if (GetById(activityId).Result.Capacity > approvedMembers.Count)
            {
                return Models.canJoin.TRUE;
            }
            
            return Models.canJoin.FALSE;
        }

        public bool canEdit(int activityId, string userId)
        {
            var user = userService.GetById(userId).Result;
            var activity = GetById(activityId).Result;
            return (user == activity.Owner);

        }
        public async Task<ICollection<MeetActivity>> GetAllByCityName(string searchString)
        {
            return await repo.GetAllByCityName(searchString);
        }
    }
}
