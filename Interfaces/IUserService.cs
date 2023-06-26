using MeetUp.Models;
using MeetUp.ViewModels;

namespace MeetUp.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<AppUser>> GetAll();
        Task<UserDetailsViewModel> GetUserDetails(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        Task<AppUser> GetById(string id);
    }
}
