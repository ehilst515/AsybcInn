using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncApp.Models;

namespace AsyncApp.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AsyncApp.Models.Hotel> Hotel { get; set; }
    }
}
