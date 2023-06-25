using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<AppUser>> GetAll();
        Task<AppUser> GetById(string id);
        bool Add(AppUser user);
        bool Update(AppUser user); 
        bool Delete(AppUser user);
        bool Save();
    }
}
