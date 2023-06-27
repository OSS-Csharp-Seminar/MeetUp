using MeetUp.Interfaces;
using MeetUp.Models;

namespace MeetUp.Services;

public class CityService : ICityService
{
    private readonly ICityRepository repo;
    public CityService(ICityRepository cityRepository)
    {
        repo = cityRepository;
    }
    
    public Task<ICollection<City>> GetAll()
    {
        return repo.GetAll();
    }

    public Task<City> GetById(int id)
    {
        return repo.GetById(id);
    }
}