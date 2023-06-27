using MeetUp.Models;

namespace MeetUp.Interfaces
{

        public interface ICityRepository
        {
                Task<ICollection<City>> GetAll();
                Task<City> GetById(int id);
                bool Add(City city);
        }
}