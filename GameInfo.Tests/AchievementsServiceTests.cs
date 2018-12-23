using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace GameInfo.Tests
{
    public class AchievementsServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoAchievements_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddAchievement_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var achievementToAdd = new AddAchievementInputModel()
                { Name = "Achievement", AcquisitionConditions = "How to get" };

                var service = new AchievementsService(context, null);
                service.Add(achievementToAdd);

                var expectedAchievement = new Achievement()
                { Name = achievementToAdd.Name, AcquisitionConditions = achievementToAdd.AcquisitionConditions };

                Assert.NotEmpty(context.Achievements);
                Assert.Equal(expectedAchievement.Name, context.Achievements.First().Name);
                Assert.Equal(expectedAchievement.AcquisitionConditions, context.Achievements.First().AcquisitionConditions);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithAchievements")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);

                var achievements = new List<Achievement>
                {
                    new Achievement() {Name = "1", AcquisitionConditions="1"},
                    new Achievement() {Name = "2", AcquisitionConditions="2"},
                    new Achievement() {Name = "3", AcquisitionConditions="3"}
                };

                context.Achievements.AddRange(achievements);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoAchievements_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoAchievements_Db_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithAchievement_ReturnsAchievement()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForById_WithAchievement")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);
                
                var achievementToAdd = new Achievement()
                {
                    Name = "Achievement",
                    AcquisitionConditions = "Redacted"
                };

                context.Achievements.Add(achievementToAdd);
                context.SaveChanges();

                var achievementFromDb = service.ById(1);

                Assert.Equal(achievementToAdd.Name, achievementFromDb.Name);
                Assert.Equal(achievementToAdd.AcquisitionConditions, achievementFromDb.AcquisitionConditions);
            }
        }

        [Fact]
        public void ByName_WithNoAchievements_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoAchievements_Db_ForByName")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);
                Assert.Null(service.ByName("Non-existing"));
            }
        }

        [Fact]
        public void ByName_WithAchievement_ReturnsAchievement()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForByName_WithAchievement")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);

                var achievementName = "Redacted";

                var achievement = new Achievement()
                {
                    Name = achievementName,
                    AcquisitionConditions = "Achievement"
                };

                context.Achievements.Add(achievement);
                context.SaveChanges();

                var achievementFromDb = service.ByName(achievementName);

                Assert.Equal(achievement.Name, achievementFromDb.Name);
                Assert.Equal(achievement.AcquisitionConditions, achievementFromDb.AcquisitionConditions);
            }
        }

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoAchievements_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesAchievement()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithAchievements_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Achievements.Add(new Achievement() { Name = "ToDelete", AcquisitionConditions = "None" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new AchievementsService(context, null);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Achievements.Count());
            }
        }
    }
}
