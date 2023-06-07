using MeetUp.Data;
using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MeetUpContext _context;
        public CategoryRepository(MeetUpContext context)
        {
            _context = context;
        }
        public bool Add(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
            _context.Remove(category);
            return Save();  
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Category.FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
