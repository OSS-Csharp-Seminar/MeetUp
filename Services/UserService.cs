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
        public bool Add(User user)
        {
           return repo.Add(user);
        }

        public bool Delete(User user)
        {
           return repo.Delete(user);
        }

        public Task<ICollection<User>> GetAll()
        {
           return repo.GetAll();
        }

        public Task<User> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(User user)
        {
           return repo.Update(user);
        }
    }
}
