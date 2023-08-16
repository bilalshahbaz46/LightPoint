using Microsoft.EntityFrameworkCore;
using Refresher.Models;
using System;

namespace Refresher
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProfileDetail> ProfileDetails { get; set; }
    }
}
