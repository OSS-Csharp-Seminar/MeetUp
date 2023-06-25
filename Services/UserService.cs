using MeetUp.Interfaces;
using MeetUp.Models;

namespace MeetUp.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository repo;
        public UserService(IUserRepository userRepository)
        {
            repo = userRepository;
        }
        public bool Add(AppUser user)
        {
           return repo.Add(user);
        }

        public bool Delete(AppUser user)
        {
           return repo.Delete(user);
        }

        public Task<ICollection<AppUser>> GetAll()
        {
           return repo.GetAll();
        }

        public Task<AppUser> GetById(string id)
        {
            return repo.GetById(id);
        }

        public bool Update(AppUser user)
        {
           return repo.Update(user);
        }
    }
}
