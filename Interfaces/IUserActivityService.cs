using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserActivityService
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<UserActivity> GetById(int id);
        Task<ICollection<AppUser>> GetUsersByActivityId(int activityId);
        Task<ICollection<UserActivity>> GetByActivityOwner(string userId);
        bool Add(string userId, int activityId);
        bool Update(UserActivity userActivity);
        bool Delete(UserActivity userActivity);
    }
}
