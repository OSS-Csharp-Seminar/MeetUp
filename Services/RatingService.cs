using MeetUp.Interfaces;
using MeetUp.Models;

namespace MeetUp.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository repo;
        public RatingService(IRatingRepository ratingRepository)
        {
            repo = ratingRepository;
        }

        public bool Add(Rating rating)
        {
            return repo.Add(rating);
        }

        public bool Delete(Rating rating)
        {
            return repo.Delete(rating);
        }

        public Task<ICollection<Rating>> GetAll()
        {
           return repo.GetAll();
        }

        public Task<Rating> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(Rating rating)
        {
            return repo.Update(rating);
        }
    }
}
