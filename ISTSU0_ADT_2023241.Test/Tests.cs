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
        private GuitarLogic guitarLogic;
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
                    Genre = Genre.Metal,
                    Guitarists= new List<Guitarist>
                    {
                        new Guitarist
                        {
                            Guitars= new List<Guitar>
                            {
                                new Guitar
                                {
                                    BodyType=BodyType.Stratocaster
                                }
                            }
                        },
                        new Guitarist
                        {
                            Guitars= new List<Guitar>
                            {
                                new Guitar
                                {
                                    BodyType=BodyType.Stratocaster
                                }
                            }
                        }
                    }
                }
        }.AsQueryable();

        private IQueryable<Guitarist> FakeGuitaristObjects() => new List<Guitarist>()
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
        }.AsQueryable();

        private IQueryable<Guitar> FakeGuitarObjects() => new List<Guitar>()
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
        }.AsQueryable();

        [SetUp]
        public void SetUp()
        {
            Mock<IBandRepository> mockBandRepo = new();
            mockBandRepo.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).Returns<Guid>((id) => Task.FromResult(FakeBandObjects().FirstOrDefault(x=>x.Id==id)));
            mockBandRepo.Setup(x => x.GetAll()).Returns(FakeBandObjects());
            mockBandRepo.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Band>())).Returns<Guid, Band>((id, band) =>
            {
                band.Id = id;
                return Task.FromResult(band);
            });
            mockBandRepo.Setup(x => x.CreateAsync(It.IsAny<Band>())).Returns<Band>((band) =>
            {
                return Task.FromResult(band);
            });
            Mock<IGuitaristRepository> mockGuitaristRepo = new();
            mockGuitaristRepo.Setup(x => x.CreateAsync(It.IsAny<Guitarist>())).Returns<Guitarist>((guitarist) =>
            {
                return Task.FromResult(guitarist);
            });
            mockGuitaristRepo.Setup(x => x.GetAll()).Returns(FakeGuitaristObjects());
            mockGuitaristRepo.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).Returns<Guid>((id) => Task.FromResult( new Guitarist { Id = id, Band = new Band { Name = "test" } }));

            Mock<IGuitarRepository> mockGuitarRepo = new();
            mockGuitarRepo.Setup(x => x.CreateAsync(It.IsAny<Guitar>())).Returns<Guitar>((guitar) => Task.FromResult(guitar));
            mockGuitarRepo.Setup(x => x.GetAll()).Returns(FakeGuitarObjects());

            this.bandLogic = new BandLogic(mockBandRepo.Object, mockGuitaristRepo.Object);
            this.guitaristLogic = new GuitaristLogic(mockGuitaristRepo.Object);
            this.guitarLogic = new GuitarLogic(mockGuitarRepo.Object);
        }

        [Test]
        public void GetOneTest()
        {
            string id = "dfc14e55-a11c-4059-81b8-5dd9dbd575d5";
            var g = guitaristLogic.GetOneAsync(Guid.Parse(id)).Result?.Id;
            Assert.That(g == Guid.Parse( id));
        }

        [Test]
        public void GetAllTest()
        {
            Assert.That(bandLogic.GetAll().ToList().Count() == 3);
        }
        [Test]
        public void CreateBandTest()
        {
            Assert.That(bandLogic.CreateAsync(new Band { Name = "Test" }).Result.Name == "Test");
            Assert.ThrowsAsync<ArgumentException>(() => bandLogic.CreateAsync(new Band { }));
        }
        [Test]
        public void CreateTestGuitarist()
        {
            Assert.That(guitaristLogic.CreateAsync(new Guitarist { Name = "Test" }).Result.Name == "Test");
            Assert.ThrowsAsync<ArgumentException>(() => guitaristLogic.CreateAsync(new Guitarist { }));
        }
        [Test]
        public void CreateGuitarTest()
        {
            Assert.That(guitarLogic.CreateAsync(new Guitar { Brand = "Test",Model = "test"}).Result.Brand == "Test");
            Assert.ThrowsAsync<ArgumentException>(() => guitarLogic.CreateAsync(new Guitar { }));
        }
        [Test]
        public void WhereDoesThisGuitaristPlayTest()
        {
            Assert.That(guitaristLogic.WhereDoesThisGuitaristPlay(Guid.Parse("dfc14e55-a11c-4059-81b8-5dd9dbd575d5")).Result?.Name == "test");
        }
        [Test]
        public void DoesThisBandHaveMultipleGuitaristsTestFalse()
        {
            Assert.That(bandLogic.DoesThisBandHaveMultipleGuitarists(Guid.Parse("97d2a327-c0b9-4f76-971f-440278bf49c0")).Result == false);
        }
        [Test]
        public void DoesThisBandHaveMultipleGuitaristsTestTrue()
        {
            Assert.That(bandLogic.DoesThisBandHaveMultipleGuitarists(Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d")).Result == true);
        }
        [Test]
        public void WhatGuitarsDoesThisBandHaveTestIsAStringList()
        {
            Assert.That(bandLogic.WhatGuitarsDoesThisBandHave(Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d")).Result is (List<string>));
        }

        [Test]
        public void WhatGuitarsDoesThisBandHaveTestIsAStringListIsABodyType()
        {
            Assert.That(Enum.TryParse<BodyType> (bandLogic.WhatGuitarsDoesThisBandHave(Guid.Parse("f19b6121-2139-4c0a-b707-940edddecc1d")).Result.FirstOrDefault(),out var _));
        }
    }
}
