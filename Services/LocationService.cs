using MeetUp.Interfaces;
using MeetUp.Models;

namespace MeetUp.Services
{
    public class LocationService : ILocationService 
    {
        private readonly ILocationRepository repo;
        public LocationService(ILocationRepository locationRepository)
        {
            repo = locationRepository;
        }

        public Location Add(Location location)
        {
            return repo.Add(location); 
        }

        public bool Delete(Location location)
        {
            return repo.Delete(location);
        }

        public Task<ICollection<Location>> GetAll()
        {
            return repo.GetAll();
        }

        public Task<Location> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(Location location)
        {
            return repo.Update(location);
        }
    }
}
