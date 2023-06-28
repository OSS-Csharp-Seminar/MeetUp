using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserActivityService
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<UserActivity> GetById(int id);
        Task<ICollection<AppUser>> ApprovedUsers(int activityId);
        bool isSubscribed(int activityId, string userId);
        Task<ICollection<UserActivity>> GetByActivityOwner(string userId);
        void Approve(string userId, int activityId);
        bool Add(UserActivity userActivity);
        bool Update(UserActivity userActivity);
        bool Delete(UserActivity userActivity);
    }
}
