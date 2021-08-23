using EFCoreCRUD.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCRUD
{
    public class UrlContext :DbContext
    {
        public DbSet<Urls> Urls{ get; set; }
        public DbSet<SubUrls> SubUrls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=EFCore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
