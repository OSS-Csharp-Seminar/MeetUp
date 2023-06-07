using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MeetUpContext _context;
        public RatingRepository(MeetUpContext context)
        {
            _context = context;
        }
        public bool Add(Rating rating)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Rating rating)
        {
            _context.Remove(rating);
            return Save();
        }

        public async Task<ICollection<Rating>> GetAll()
        {
            return await _context.Rating.Include(r=> r.Reviewee).ToListAsync();
        }

        public async Task<Rating> GetById(int id)
        {
            return await _context.Rating.Include(r=>r.Reviewee).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Rating rating)
        {
            _context.Update(rating);
            return Save();
        }
    }
}
