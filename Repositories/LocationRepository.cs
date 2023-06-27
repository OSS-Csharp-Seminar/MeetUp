﻿using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MeetUpContext _context;
        public LocationRepository(MeetUpContext context)
        {
            _context = context;
        }
        public Location Add(Location location)
        {
            var created = _context.Add(location).Entity;
            Save();
            return created;
        }

        public bool Delete(Location location)
        {
            _context.Remove(location);
            return Save();
        }

        public async Task<ICollection<Location>> GetAll()
        {
            return await _context.Location.Include(l => l.City).ToListAsync();
        }

        public async Task<Location> GetById(int id)
        {
            return await _context.Location.Include(l => l.City).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Location location)
        {
            _context.Update(location);
            return Save();  
        }
    }
}
