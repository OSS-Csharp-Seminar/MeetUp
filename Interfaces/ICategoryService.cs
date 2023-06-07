using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAll();
        Task<Category> GetById(int id);
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
    }
}
