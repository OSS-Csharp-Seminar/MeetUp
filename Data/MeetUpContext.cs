using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetUp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MeetUp.Data
{
    public class MeetUpContext : IdentityDbContext<AppUser>
    {
        public MeetUpContext (DbContextOptions<MeetUpContext> options)
            : base(options)
        {
        }

        public DbSet<MeetUp.Models.AppUser>? User { get; set; }

        public DbSet<MeetUp.Models.Category>? Category { get; set; }

        public DbSet<MeetUp.Models.Location>? Location { get; set; }

        public DbSet<MeetUp.Models.Rating>? Rating { get; set; }

        public DbSet<MeetUp.Models.MeetActivity>? MeetActivity { get; set; }

        public DbSet<MeetUp.Models.UserActivity>? UserActivity { get; set; }

        public DbSet<City> City { get; set; }
    }
}
