using MeetUp.Interfaces;

namespace MeetUp.Services
{
    public class MeetActivityService :IMeetActivityService
    {
        private readonly IMeetActivityRepository repo;
        public MeetActivityService(IMeetActivityRepository meetActivityRepository)
        {
            repo = meetActivityRepository;
        }

        public bool Add(Models.MeetActivity meetActivity)
        {
            return repo.Add(meetActivity);  
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

        public String Validate(Models.MeetActivity meetActivity)
        {
            if (meetActivity.Time <= DateTime.Now)
            {
                return "Date cannot be in the past.";
            }

            return "";
        }
    }
}
