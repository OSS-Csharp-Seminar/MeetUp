using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetUp.Models;

namespace MeetUp.Data
{
    public class MeetUpContext : DbContext
    {
        public MeetUpContext (DbContextOptions<MeetUpContext> options)
            : base(options)
        {
        }

        public DbSet<MeetUp.Models.User> User { get; set; } = default!;

        public DbSet<MeetUp.Models.Category>? Category { get; set; }

        public DbSet<MeetUp.Models.Location>? Location { get; set; }

        public DbSet<MeetUp.Models.Rating>? Rating { get; set; }

        public DbSet<MeetUp.Models.MeetActivity>? MeetActivity { get; set; }

        public DbSet<MeetUp.Models.UserActivity>? UserActivity { get; set; }
    }
}
