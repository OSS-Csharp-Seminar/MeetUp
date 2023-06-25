using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserActivityService
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<UserActivity> GetById(int id);
        Task<ICollection<AppUser>> GetUsersByActivityId(int activityId);
        bool Add(UserActivity userActivity);
        bool Update(UserActivity userActivity);
        bool Delete(UserActivity userActivity);
    }
}
