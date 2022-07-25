using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvcApp.Models
{
    public class SalesWebMvcAppContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=saleswebmvcappdb;Trusted_Connection=True",
                    options => options.EnableRetryOnFailure());
        }


        public SalesWebMvcAppContext (DbContextOptions<SalesWebMvcAppContext> options)
            : base(options)
        {
        }

        public DbSet<Departments> Departments { get; set; }

        public DbSet<Saller> Seller { get; set; }

        public DbSet<SalesRecord> SalesRecords { get; set; }

    }



}
