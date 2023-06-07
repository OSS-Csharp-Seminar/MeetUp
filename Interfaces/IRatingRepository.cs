using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IRatingRepository
    {
        Task<ICollection<Rating>> GetAll();
        Task<Rating> GetById(int id);
        bool Add(Rating rating);
        bool Update(Rating rating);
        bool Delete(Rating rating);
        bool Save();
    }
}
