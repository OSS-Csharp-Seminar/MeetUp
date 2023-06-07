using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserActivityRepository
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<UserActivity> GetById(int id);
        bool Add(UserActivity userActivity);
        bool Update(UserActivity userActivity);
        bool Delete(UserActivity userActivity);
        bool Save();
    }
}
