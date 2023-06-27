using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserActivityRepository
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<ICollection<AppUser>> ApprovedUsers(int activityId);
        Task<UserActivity> GetById(int id);
        bool Add(UserActivity userActivity);
        bool Update(UserActivity userActivity);
        bool Delete(UserActivity userActivity);
        bool Save();
        Task<ICollection<UserActivity>> GetActivitiesByUserId(string id);
        Task<UserActivity> GetByUserAndActivity(string userId, int activityId);
        Task<ICollection<UserActivity>> GetByActivityOwner(string userId);

    }
}
