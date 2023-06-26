using MeetUp.Models;
using MeetUp.ViewModels;

namespace MeetUp.Interfaces
{
    public interface IMeetActivityService
    {
        Task<ICollection<MeetActivity>> GetAll();
        Task<MeetActivity> GetById(int id);
        bool Add(MeetActivityCreateModel meetActivity, string userId);
        bool Update(MeetActivity meetActivity);
        bool Delete(MeetActivity meetActivity);
        String Validate(MeetActivityCreateModel meetActivity);
        bool canJoin(int activityId, string userId, bool isAuthenticated);
    }
}
