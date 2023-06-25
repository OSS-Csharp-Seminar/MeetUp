using MeetUp.Interfaces;
using MeetUp.ViewModels;

namespace MeetUp.Services
{
    public class MeetActivityService :IMeetActivityService
    {
        private readonly IMeetActivityRepository repo;
        public MeetActivityService(IMeetActivityRepository meetActivityRepository)
        {
            repo = meetActivityRepository;
        }

        public bool Add(MeetActivityViewModel meetActivity)
        {
            return repo.Add(MeetActivityViewModel.to(meetActivity));  
        }

        public bool Delete(Models.MeetActivity meetActivity)
        {
            return repo.Delete(meetActivity);
        }

        public Task<ICollection<Models.MeetActivity>> GetAll()
        {
            return repo.GetAll();
        }

        public Task<Models.MeetActivity> GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool Update(Models.MeetActivity meetActivity)
        {
            return repo.Update(meetActivity);   
        }

        public String Validate(MeetActivityViewModel meetActivity)
        {
            if (meetActivity.Time <= DateTime.Now)
            {
                return "Date cannot be in the past.";
            }

            return "";
        }
    }
}
