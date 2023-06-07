using MeetUp.Interfaces;
using MeetUp.Models;

namespace MeetUp.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository repo;
        public UserActivityService(IUserActivityRepository userActivityRepository)
        {
            repo = userActivityRepository;
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
