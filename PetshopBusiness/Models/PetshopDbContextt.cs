using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetshopBusiness.Models
{
    public class PetshopDbContextt : DbContext
    {
        public PetshopDbContextt(DbContextOptions<PetshopDbContextt> options)
            : base(options)
        {
        }

        public PetshopDbContextt() : base()
        {
        }

        public DbSet<ClientUser> ClientUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=Petshop2;User ID=Rodrigues;Password=@Puta00001");
            //optionsBuilder.UseSqlServer(System.Configuration. "DevConnection");
        }
    }
}
