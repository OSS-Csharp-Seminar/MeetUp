using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories
{
    public class UserActivityRepository : IUserActivityRepository 
    {
        private readonly MeetUpContext _context;
        public UserActivityRepository(MeetUpContext context)
        {
            _context = context;
        }

        public bool Add(UserActivity userActivity)
        {
            if (userActivity is null)
            {
                throw new ArgumentNullException(nameof(userActivity));
            }
            _context.Add(userActivity);
            return Save();
          
        }

        public bool Delete(UserActivity userActivity)
        {
            _context.Remove(userActivity);
            return Save();
        }

        public async Task<ICollection<UserActivity>> GetAll()
        {
            return await _context.UserActivity.Include(ua => ua.User).Include(ua=>ua.Activity).ToListAsync();
        }

        public async Task<ICollection<AppUser>> GetUsersByActivityId(int activityId)
        {
            return _context.UserActivity.Where(userActivity => userActivity.ActivityId == activityId)
                .Select(userActivity => userActivity.User).ToList();
        }

        public async Task<UserActivity> GetById(int id)
        {
            return await _context.UserActivity.Include(ua => ua.User).Include(ua => ua.Activity).FirstOrDefaultAsync(ua => ua.Id == id);
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(UserActivity userActivity)
        {
            _context.Update(userActivity);
            return Save();
        }
    }
}
