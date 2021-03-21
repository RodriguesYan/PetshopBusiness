using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetshopDB.Models
{
    public class PetshopDbContext : DbContext
    {
        public PetshopDbContext(DbContextOptions<PetshopDbContext> options)
            : base(options)
        {
        }

        public PetshopDbContext() : base()
        {
        }

        public DbSet<ClientUser> ClientUser { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ClientUserAddress> ClientUserAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=Petshop2;User ID=Rodrigues;Password=Rodrigues@0002");
            //optionsBuilder.UseSqlServer(System.Configuration. "DevConnection");
        }
    }
}
