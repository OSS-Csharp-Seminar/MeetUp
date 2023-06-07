using MeetUp.Interfaces;
using MeetUp.Models;

namespace MeetUp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repo;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            repo = categoryRepository;
        }

        public bool Add(Category category)
        {
            return repo.Add(category);
        }

        public bool Delete(Category category)
        {
            return repo.Delete(category);
        }

        public Task<ICollection<Category>> GetAll()
        {
            return repo.GetAll();
        }

        public Task<Category> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(Category category)
        {
            return repo.Update(category);
        }
    }
}
