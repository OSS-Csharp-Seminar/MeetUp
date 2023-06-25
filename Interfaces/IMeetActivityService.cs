using MeetUp.Models;
using MeetUp.ViewModels;

namespace MeetUp.Interfaces
{
    public interface IMeetActivityService
    {
        Task<ICollection<MeetActivity>> GetAll();
        Task<MeetActivity> GetById(int id);
        bool Add(MeetActivityCreateModel meetActivity);
        bool Update(MeetActivity meetActivity);
        bool Delete(MeetActivity meetActivity);
        String Validate(MeetActivityCreateModel meetActivity);
    }
}
