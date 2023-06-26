using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeetUpContext _context;
        public UserRepository(MeetUpContext context)
        {
            _context = context;
        }
        public async Task<ICollection<AppUser>> GetAll()
        {
            return await _context.User.ToListAsync();
        }
        public bool Add(AppUser user)
        {
            _context.Add(user);
            return Save();
        }
        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.User.FirstOrDefaultAsync();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
