﻿using MeetUp.Models;

namespace MeetUp.Interfaces
{
    public interface ILocationService
    {
        Task<ICollection<Location>> GetAll();
        Task<Location> GetById(int id);
        Task<Location> GetByAddressAndCityId(string address, int cityId);
        Location Add(Location location);
        bool Update(Location location);
        bool Delete(Location location);
    }
}
