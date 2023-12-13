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
            this.Database.EnsureCreated();
        }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Guitar> Guitars { get; set; }
        public DbSet<Guitarist> Guitarists { get; set; }

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
                    Id = Guid.Parse("97d2a327-c0b9-4f76-971f-440278bf49c0"),
                    Name= "Rage Against The Machine",
                    Genre = Genre.RapMetal
                },
                new Band()
                {
                    Id = Guid.Parse("d86ae663-3868-45e1-8c4b-a5f6a2a266c9"),
                    Name= "DragonForce",
                    Genre = Genre.Metal
                },
                new Band()
                 {
                    Id = Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d"),
                    Name= "Lorna Shore",
                    Genre = Genre.Metal
                }
            };
            List<Guitarist> guitarists = new List<Guitarist>()
            {
                new Guitarist()
                {
                    Id=Guid.Parse("dfc14e55-a11c-4059-81b8-5dd9dbd575d5"),
                    Name="Tom Morello",
                    Age=59,
                    BandId=Guid.Parse("97d2a327-c0b9-4f76-971f-440278bf49c0"),
                },
                new Guitarist()
                {
                    Id=Guid.Parse("f95ed1d4-ea19-428a-a5c6-9c48e043d0a8"),
                    Name="Herman Li",
                    Age=47,
                    BandId=Guid.Parse("d86ae663-3868-45e1-8c4b-a5f6a2a266c9"),
                },
                new Guitarist()
                {
                    Id = Guid.Parse("dcf3981c-ead3-4f12-9881-3e882d57f288"),
                    Name="Adam De Micco",
                    Age=34,
                    BandId=Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d"),
                },
                new Guitarist()
                {
                    Id = Guid.Parse("634fd3c7-a75d-4a2b-99df-d1ee74e051a8"),
                    Name="Andrew O'Connor",
                    Age=32,
                    BandId=Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d"),
                }
            };
            List<Guitar> guitars = new List<Guitar>()
            {
                new Guitar()
                {
                    Id=Guid.Parse("f61aa4c3-21c9-4499-8832-8b7670ab8d6e"),
                    Brand="Gibson",
                    BodyType=BodyType.LesPaul,
                    Model="Standard",
                    GuitaristId=Guid.Parse("dfc14e55-a11c-4059-81b8-5dd9dbd575d5")
                },
                new Guitar()
                {
                    Id=Guid.Parse("f444cb23-d757-497a-95c1-2738c39a9ec1"),
                    Brand="Ibanez",
                    BodyType=BodyType.LesPaul,
                    Model="Artstar Custom",
                    GuitaristId=Guid.Parse("dfc14e55-a11c-4059-81b8-5dd9dbd575d5")
                },
                new Guitar()
                {
                    Id=Guid.Parse("fe2703a7-c132-48c0-b8ed-2ef7406264ce"),
                    Brand="Ibanez",
                    BodyType=BodyType.SuperStrat,
                    Model="E-Gen",
                    GuitaristId=Guid.Parse("f95ed1d4-ea19-428a-a5c6-9c48e043d0a8")
                },
                new Guitar()
                {
                    Id=Guid.Parse("dcf3981c-ead3-4f12-9881-3e882d57f288"),
                    Brand="Ibanez",
                    BodyType=BodyType.SuperStrat,
                    Model="RGDR4327 Prestige",
                    GuitaristId=Guid.Parse("dcf3981c-ead3-4f12-9881-3e882d57f288")
                },
                new Guitar()
                {
                    Id=Guid.Parse("98c8a386-e133-4a34-949f-5a57bcc75acf"),
                    Brand="Solar Guitars",
                    BodyType=BodyType.SuperStrat,
                    Model="S1.6 PB-27 ETC",
                    GuitaristId=Guid.Parse("634fd3c7-a75d-4a2b-99df-d1ee74e051a8")
                }
            };
            modelBuilder.Entity<Guitar>().HasData(guitars);
            modelBuilder.Entity<Band>().HasData(bands);
            modelBuilder.Entity<Guitarist>().HasData(guitarists);
        }
    }
}
