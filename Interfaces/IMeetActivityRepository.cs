﻿using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface IMeetActivityRepository
    {
        Task<ICollection<MeetActivity>> GetAll();
        Task<ICollection<MeetActivity>> GetAllByCityName(string searchString);
        Task<MeetActivity> GetById(int id);
        MeetActivity Add(MeetActivity meetActivity);
        bool Update(MeetActivity meetActivity);
        bool Delete(MeetActivity meetActivity);
        bool Save();
    }
}
