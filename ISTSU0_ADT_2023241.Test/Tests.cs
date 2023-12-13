using ISTSU0_ADT_2023241.Logic;
using ISTSU0_ADT_2023241.Models;
using ISTSU0_ADT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Test
{
    [TestFixture]
    public class Tests
    {
        private GuitaristLogic guitaristLogic;
        private BandLogic bandLogic;
        private IQueryable<Band> FakeBandObjects() => new List<Band>()
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
        }.AsQueryable();
        [SetUp]
        public void SetUp()
        {
            Mock<IGuitaristRepository> mockGuitaristRepo = new();
            Mock<IBandRepository> mockBandRepo = new();
            mockBandRepo.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).Returns<Guid>((id) => Task.FromResult(FakeBandObjects().FirstOrDefault(x => x.Id == id)));
            mockBandRepo.Setup(x => x.GetAll()).Returns((FakeBandObjects()));
            mockBandRepo.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Band>())).Returns<Guid, Band>((id, band) =>
            {
                band.Id = id;
                return Task.FromResult(band);
            });
            mockBandRepo.Setup(x => x.CreateAsync(It.IsAny<Band>())).Returns<Band>((band) =>
            {
                return Task.FromResult(band);
            });
            this.bandLogic = new BandLogic(mockBandRepo.Object, mockGuitaristRepo.Object);
            this.guitaristLogic = new GuitaristLogic(mockGuitaristRepo.Object);
            mockGuitaristRepo.Setup(x => x.CreateAsync(It.IsAny<Guitarist>())).Returns<Guitarist>((guitarist) =>
            {
                return Task.FromResult(guitarist);
            });
        }
        [Test]
        public void GetOneTest()
        {
            Assert.That(bandLogic.GetOneAsync(Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d")).Result.Name == "Lorna Shore");
        }

        [Test]
        public void GetAllTest1()
        {
            Assert.That(bandLogic.GetAll().ToList().Count() == 3);
        }
        [Test]
        public void CreateTest()
        {
            Assert.That(bandLogic.CreateAsync(new Band { Name = "Test" }).Result.Name == "Test");
        }
        [Test]
        public void CreateTestGuitarist()
        {
            Assert.That(guitaristLogic.CreateAsync(new Guitarist { Name = "Test" }).Result.Name == "Test");
        }
    }
}
