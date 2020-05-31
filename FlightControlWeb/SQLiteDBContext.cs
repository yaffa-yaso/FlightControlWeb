using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb;
using FlightControlWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace SQLiteAspNetCoreDemo
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<JsonFP> FlightPlans { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sqlitedemo.db");
    }
}
