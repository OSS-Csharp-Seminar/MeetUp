using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<AppUser>> GetAll();
        Task<AppUser> GetById(int id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
    }
}
