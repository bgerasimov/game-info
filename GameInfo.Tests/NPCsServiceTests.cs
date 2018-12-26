using GameInfo.Data;
using GameInfo.Models;
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
    public class NPCsServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoNPCs_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddNPC_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var NPCToAdd = new AddNPCInputModel()
                { Name = "NPC Name" };

                var service = new NPCsService(context, null);
                service.Add(NPCToAdd);

                var expectedNPC = new NPC()
                { Name = NPCToAdd.Name };

                Assert.NotEmpty(context.NPCs);
                Assert.Equal(expectedNPC.Name, context.NPCs.First().Name);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithNPCs")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);

                var NPCs = new List<NPC>
                {
                    new NPC() {Name = "1"},
                    new NPC() {Name = "2"},
                    new NPC() {Name = "3"}
                };

                context.NPCs.AddRange(NPCs);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoNPCs_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoNPCs_Db_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithNPC_ReturnNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForById_WithNPC")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);

                var NPCToAdd = new NPC()
                {
                    Name = "NPC added"
                };

                context.NPCs.Add(NPCToAdd);
                context.SaveChanges();

                var NPCFromDb = service.ById(1);

                Assert.Equal(NPCToAdd.Name, NPCFromDb.Name);
            }
        }

        [Fact]
        public void ByName_WithNoNPCs_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoNPCs_Db_ForByName")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);
                Assert.Null(service.ByName("Non-existing"));
            }
        }

        [Fact]
        public void ByName_WithNPC_ReturnsNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForByName_WithNPC")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);

                var NPCName = "NPC Name";

                var NPC = new NPC()
                {
                    Name = NPCName
                };

                context.NPCs.Add(NPC);
                context.SaveChanges();

                var NPCFromDb = service.ByName(NPCName);

                Assert.Equal(NPC.Name, NPCFromDb.Name);
            }
        }

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoNPCs_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithNPC_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.NPCs.Add(new NPC() { Name = "ToDelete" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new NPCsService(context, null);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.NPCs.Count());
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void AddItem_AddsToNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForAddItem_ToNPC")
                .Options;

            var itemName = "Item Name";

            var npc = new NPC() { Name = "NPC Name" };
            var item = new Item() { Name = itemName, AcquiredFrom = "None", Usage = "None" };

            using (var context = new GameInfoContext(options))
            {
                context.NPCs.Add(npc);
                context.Items.Add(item);
                context.SaveChanges();

                var model = new AddItemToNPCInputModel() { NPCId = 1, ItemName = itemName };

                var service = new NPCsService(context, new ItemsService(context));

                Assert.True(service.AddItemToNPC(model));
            }
        }

        [Fact]
        public void AddQuest_AddsToNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForAddQuest_ToNPC")
                .Options;

            var npc = new NPC() { Name = "NPC QuestAdd" };
            var quest = new Quest() { Title = "QuestTitle", CompletionCondition = "None" };

            using (var context = new GameInfoContext(options))
            {
                context.NPCs.Add(npc);
                context.Quests.Add(quest);
                context.SaveChanges();

                var model = new AddQuestToNPCInputModel() { NPCId = 1 };

                var service = new NPCsService(context, null);

                Assert.True(service.AddQuestToNPC(model, quest));
            }
        }

        [Fact]
        public async Task RemoveItem_RemovesFromNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForRemoveItem_FromNPC")
                .Options;

            var npc = new NPC() { Name = "NPC RemoveItem" };
            var item = new Item() { Name = "Item to remove" };

            using (var context = new GameInfoContext(options))
            {
                npc.Loot.Add(item);
                context.NPCs.Add(npc);
                context.SaveChanges();

                var service = new NPCsService(context, null);

                await service.RemoveItem(npc, item);

                var NPCFromDb = context.NPCs.First();

                Assert.True(NPCFromDb.Loot.Count == 0);
            }
        }

        [Fact]
        public async Task RemoveQuest_RemovesFromNPC()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForRemoveQuest_FromNPC")
                .Options;

            var npc = new NPC() { Name = "NPC RemoveQuest" };
            var quest = new Quest() { Title = "To remove" };

            using (var context = new GameInfoContext(options))
            {
                npc.Quests.Add(quest);
                context.NPCs.Add(npc);
                context.SaveChanges();

                var service = new NPCsService(context, null);

                await service.RemoveQuest(npc, quest);

                var NPCFromDb = context.NPCs.First();

                Assert.True(NPCFromDb.Quests.Count == 0);
            }
        }
    }
}
