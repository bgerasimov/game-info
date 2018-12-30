using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.Enums;
using GameInfo.Models.InputModels;
using GameInfo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GameInfo.Tests
{
    public class RacesServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoRaces_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new RacesService(context, null);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddRace_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var raceToAdd = new AddRaceInputModel()
                { Name = "RaceName", Description = "Description" };

                var service = new RacesService(context, null);
                service.Add(raceToAdd);

                var expectedRace = new Race()
                { Name = raceToAdd.Name, Description = raceToAdd.Description };

                Assert.NotEmpty(context.Races);
                Assert.Equal(expectedRace.Name, context.Races.First().Name);
                Assert.Equal(expectedRace.Description, context.Races.First().Description);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithRaces")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new RacesService(context, null);

                var races = new List<Race>
                {
                    new Race() {Name = "1", Description="1"},
                    new Race() {Name = "2", Description="2"},
                    new Race() {Name = "3", Description="3"}
                };

                context.Races.AddRange(races);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoRaces_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoRaces_DbFor_ById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new RacesService(context, null);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithRace_ReturnsRace()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "DbFor_ById_WithRace")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new RacesService(context, null);

                var raceToAdd = new Race()
                {
                    Name = "Race",
                    Description = "None"
                };

                context.Races.Add(raceToAdd);
                context.SaveChanges();

                var raceFromDb = service.ById(1);

                Assert.Equal(raceToAdd.Name, raceFromDb.Name);
                Assert.Equal(raceToAdd.Description, raceFromDb.Description);
            }
        }

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoRaces_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new RacesService(context, null);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesRace()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithRace_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Races.Add(new Race() { Name = "ToDelete", Description = "No Desc" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new RacesService(context, null);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Races.Count());
            }
        }

        [Fact]
        public void AddProfessionToRace_AddsDataProperly()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForAddProfToRace")
                .Options;

            var professionName = "Prof Name";

            using (var context = new GameInfoContext(options))
            {
                var race = new Race()
                {
                    Name = "race",
                    Description = "none"
                };

                var profession = new Profession()
                {
                    Name = professionName,
                    ClassRole = ClassRole.Damage,
                    CombatType = CombatType.Mage,
                    UsableWeapon = WeaponType.Staff
                };

                context.Races.Add(race);
                context.Professions.Add(profession);
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var raceFromDb = context.Races.First();
                
                var profService = new ProfessionsService(context);
                var service = new RacesService(context, profService);

                var model = new AddProfessionToRaceInputModel()
                {
                    RaceId = raceFromDb.Id,
                    ProfessionName = professionName                    
                };

                Assert.True(service.AddProfessionToRace(model));
            }
        }
    }
}
