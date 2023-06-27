using MeetUp.Models;

namespace MeetUp.Interfaces
{

    public interface ICityService
    {
        Task<ICollection<City>> GetAll();
        Task<City> GetById(int id);
        bool Add(City city);
    }
}