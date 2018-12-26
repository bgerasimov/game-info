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
using System.Threading.Tasks;
using Xunit;

namespace GameInfo.Tests
{
    public class ProfessionsServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoProfessions_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddProfession_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var professionToAdd = new AddProfessionInputModel()
                {
                    Name = "ProfessionAdd",
                    UsableWeaponType = WeaponType.Axe.ToString(),
                    CombatType = CombatType.Melee.ToString(),
                    ClassRole = ClassRole.Damage.ToString()
                };

                var service = new ProfessionsService(context);
                service.Add(professionToAdd);

                var expectedProfession = new Profession()
                {
                    Name = professionToAdd.Name
                };

                Assert.NotEmpty(context.Professions);
                Assert.Equal(expectedProfession.Name, context.Professions.First().Name);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithProfessions")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);

                var professions = new List<Profession>
                {
                    new Profession() {Name = "1", },
                    new Profession() {Name = "2", },
                    new Profession() {Name = "3", }
                };

                context.Professions.AddRange(professions);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoProfessions_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoProfessions_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithProfession_ReturnsProfession()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "WithProfession_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);

                var professionToAdd = new Profession()
                {
                    Name = "Profession"
                };

                context.Professions.Add(professionToAdd);
                context.SaveChanges();

                var professionFromDb = service.ById(1);

                Assert.Equal(professionToAdd.Name, professionFromDb.Name);
            }
        }

        [Fact]
        public void ByName_WithNoProfessions_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoProfessions_ForByName")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);
                Assert.Null(service.ByName("Non-existing"));
            }
        }

        [Fact]
        public void ByName_WithProfession_ReturnsProfession()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "ForByName_WithProfession")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);

                var professionName = "Prof Name";

                var profession = new Profession()
                {
                    Name = professionName
                };

                context.Professions.Add(profession);
                context.SaveChanges();

                var professionFromDb = service.ByName(professionName);

                Assert.Equal(profession.Name, professionFromDb.Name);
            }
        }

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoProfessions_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesProfession()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "WithProfession_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Professions.Add(new Profession() { Name = "ToDelete" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new ProfessionsService(context);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Professions.Count());
            }
        }

        [Fact]
        public async Task RemoveRace_WithData_RemovesFromProfession()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "WithRaceInProf_ForRemoveRace")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var profession = new Profession()
                {
                    Name = "ProfWithRace"
                };

                var race = new Race()
                {
                    Name = "RaceWithProf"
                };

                context.Professions.Add(profession);
                context.Races.Add(race);
                await context.SaveChangesAsync();

                var profFromDb = context.Professions.First();
                var raceFromDb = context.Races.First();

                var raceProfession = new RaceProfession()
                {
                    Race = raceFromDb,
                    RaceId = raceFromDb.Id,
                    Profession = profFromDb,
                    ProfessionId = profFromDb.Id
                };

                profFromDb.Races.Add(raceProfession);

                await context.SaveChangesAsync();

                var service = new ProfessionsService(context);
                await service.RemoveRace(profFromDb, raceFromDb);

                Assert.DoesNotContain(raceProfession, profFromDb.Races);
            }
        }
    }
}
