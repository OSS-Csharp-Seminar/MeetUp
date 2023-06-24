﻿using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories
{
    public class MeetActivityRepository : IMeetActivityRepository
    {
        private readonly MeetUpContext _context;
        public MeetActivityRepository(MeetUpContext context)
        {
            _context = context;
        }
        public bool Add(MeetActivity meetActivity)
        {
            _context.Add(meetActivity);
            return Save();
        }

        public bool Delete(MeetActivity meetActivity)
        {
            _context.Remove(meetActivity);
            return Save();
        }

        public async Task<ICollection<MeetActivity>> GetAll()
        {
            return await _context.MeetActivity.Include(ma => ma.Category).Include(ma => ma.Location).ToListAsync();
        }

        public async Task<MeetActivity> GetById(int id)
        {
            return await _context.MeetActivity.Include(ma => ma.Category).Include(ma => ma.Location).FirstOrDefaultAsync(ma => ma.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(MeetActivity meetActivity)
        {
            _context.Update(meetActivity);
            return Save();  
        }
    }
}