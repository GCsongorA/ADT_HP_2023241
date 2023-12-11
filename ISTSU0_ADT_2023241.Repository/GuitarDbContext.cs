using ISTSU0_ADT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Repository
{
    public class GuitarDbContext : DbContext
    {
        public GuitarDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Guitar> Guitars { get; set; }
        public DbSet<Guitarist> Guitarists { get; set; }
        public DbSet<GuitarStore> GuitarStores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("GuitarDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Band> bands = new List<Band>()
            {
                new Band()
                {
                    Id = Guid.NewGuid(),
                    Name= "Rage Against The Machine",
                    Genre = Genre.RapMetal
                }
            };

            modelBuilder.Entity<Band>().HasData(bands);
        }
    }
}
