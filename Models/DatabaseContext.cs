using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class DatabaseContext : DbContext
    {
        //public DbSet&lt;Person&gt; Persons { get; set; }
        public DbSet<WeatherInfo> WeatherInfo { get; set; }
        public DbSet<weatherForcast> weatherForcast { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDatabase.sqlite");
        }
    }
}
