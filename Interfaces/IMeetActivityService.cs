using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IMeetActivityService
    {
        Task<ICollection<MeetActivity>> GetAll();
        Task<MeetActivity> GetById(int id);
        bool Add(MeetActivity meetActivity);
        bool Update(MeetActivity meetActivity);
        bool Delete(MeetActivity meetActivity);
    }
}
