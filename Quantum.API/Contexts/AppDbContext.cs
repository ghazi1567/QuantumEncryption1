using Microsoft.EntityFrameworkCore;
using Quantum.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.API.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserBase> UserBases { get; set; }
        public DbSet<TempTable> TempTables { get; set; }
    }
}
