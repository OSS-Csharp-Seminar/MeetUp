using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface ILocationRepository
    {
        Task<ICollection<Location>> GetAll();
        Task<Location> GetById(int id);
        bool Add(Location location);
        bool Update(Location location);
        bool Delete(Location location);
        bool Save();
    }
}
