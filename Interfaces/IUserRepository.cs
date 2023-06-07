using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAll();
        Task<User> GetById(int id);
        bool Add(User user);
        bool Update(User user); 
        bool Delete(User user);
        bool Save();
    }
}
