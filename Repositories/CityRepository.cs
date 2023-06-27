using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories;

public class CityRepository : ICityRepository
{
    private readonly MeetUpContext _context;
    
    public CityRepository(MeetUpContext context)
    {
        _context = context;
    }

    public async Task<ICollection<City>> GetAll()
    {
        return await _context.City.ToListAsync();
    }

    public async Task<City> GetById(int id)
    {
        return await _context.City.FirstOrDefaultAsync();
    }
    
    public bool Add(City city)
    {
        _context.Add(city);
        return Save();
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}