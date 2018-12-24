using GameInfo.Data;
using GameInfo.Models;
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
    public class GuidesServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoGuides_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new GuidesService(context);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddGuide_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var guideToAdd = new AddGuideInputModel()
                { GuideTitle = "Guide", GuideContent = "Content" };

                var user = new GameInfoUser() { UserName = "UserDummy" };

                var service = new GuidesService(context);
                service.Add(guideToAdd, user);

                var expectedGuide = new Guide()
                { Title = guideToAdd.GuideTitle, Content = guideToAdd.GuideContent };

                Assert.NotEmpty(context.Guides);
                Assert.Equal(expectedGuide.Title, context.Guides.First().Title);
                Assert.Equal(expectedGuide.Content, context.Guides.First().Content);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithGuides")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new GuidesService(context);

                var guides = new List<Guide>
                {
                    new Guide() {Title = "1", Content="1"},
                    new Guide() {Title = "2", Content="2"},
                    new Guide() {Title = "3", Content="3"}
                };

                context.Guides.AddRange(guides);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoGuides_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoGuides_Db_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new GuidesService(context);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithGuide_ReturnsGuide()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForById_WithGuide")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new GuidesService(context);

                var guideToAdd = new Guide()
                {
                    Title = "GuideTitle",
                    Content = "GuideContent"
                };

                context.Guides.Add(guideToAdd);
                context.SaveChanges();

                var guideFromDb = service.ById(1);

                Assert.Equal(guideToAdd.Title, guideFromDb.Title);
                Assert.Equal(guideToAdd.Content, guideFromDb.Content);
            }
        }
        
        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoGuides_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new GuidesService(context);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesGuide()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithGuides_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Guides.Add(new Guide() { Title = "ToDelete", Content = "None" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new GuidesService(context);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Guides.Count());
            }
        }
    }
}
