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
    public class QuestsServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoQuests_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddQuest_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var questToAdd = new AddQuestInputModel()
                { Title = "QuestTitle", QuestText = "QuestText", CompletionCondition="None" };

                questToAdd.QuestGiver = null;

                var service = new QuestsService(context, null);
                service.Add(questToAdd);

                var expectedQuest = new Quest()
                { Title = questToAdd.Title, QuestText = questToAdd.QuestText, CompletionCondition = questToAdd.CompletionCondition };

                Assert.NotEmpty(context.Quests);
                Assert.Equal(expectedQuest.Title, context.Quests.First().Title);
                Assert.Equal(expectedQuest.QuestText, context.Quests.First().QuestText);
                Assert.Equal(expectedQuest.CompletionCondition, context.Quests.First().CompletionCondition);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithQuests")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);

                var quests = new List<Quest>
                {
                    new Quest() {Title = "1", QuestText="1", CompletionCondition="1"},
                    new Quest() {Title = "2", QuestText="2", CompletionCondition="2"},
                    new Quest() {Title = "3", QuestText="3", CompletionCondition="3"}
                };

                context.Quests.AddRange(quests);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoQuests_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoQuests_DbFor_ById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithQuest_ReturnsQuest()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForById_WithQuest")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);

                var questToAdd = new Quest()
                {
                    Title = "Title",
                    QuestText = "Text",
                    CompletionCondition = "None"
                };

                context.Quests.Add(questToAdd);
                context.SaveChanges();

                var questFromDb = service.ById(1);

                Assert.Equal(questToAdd.Title, questFromDb.Title);
                Assert.Equal(questToAdd.QuestText, questFromDb.QuestText);
                Assert.Equal(questToAdd.CompletionCondition, questFromDb.CompletionCondition);
            }
        }

        [Fact]
        public void ByName_WithNoQuest_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoQuest_DbFor_ByName")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);
                Assert.Null(service.ByName("Non-existing"));
            }
        }

        [Fact]
        public void ByName_WithQuest_ReturnsQuest()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "DbFor_ByName_WithQuest")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);

                var questTitle = "QuestTitle";

                var quest = new Quest()
                {
                    Title = questTitle,
                    QuestText = "TextForQuest",
                    CompletionCondition = "None"
                };

                context.Quests.Add(quest);
                context.SaveChanges();

                var questFromDb = service.ByName(questTitle);

                Assert.Equal(quest.Title, questFromDb.Title);
                Assert.Equal(quest.QuestText, questFromDb.QuestText);
                Assert.Equal(quest.CompletionCondition, questFromDb.CompletionCondition);
            }
        }

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoQuests_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesQuest()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithQuest_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Quests.Add(new Quest() { Title = "ToDelete", QuestText = "None", CompletionCondition = "None" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new QuestsService(context, null);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Quests.Count());
            }
        }
    }
}
