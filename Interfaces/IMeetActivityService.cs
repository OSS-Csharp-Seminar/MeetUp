using MeetUp.Models;
using MeetUp.ViewModels;

namespace MeetUp.Interfaces
{
    public interface IMeetActivityService
    {
        Task<ICollection<MeetActivity>> GetAll();
        Task<MeetActivity> GetById(int id);
        bool Add(MeetActivityCreateModel meetActivity, string userId);
        bool Update(MeetActivityEditModel meetActivity);
        bool Delete(MeetActivity meetActivity);
        String ValidateDate(DateTime activityTime);
        canJoin canJoin(int activityId, string userId, bool isAuthenticated);
        bool canEdit(int activityId, string userId);

        Task<ICollection<MeetActivity>> GetAllByCityName(string searchString);
    }
}
