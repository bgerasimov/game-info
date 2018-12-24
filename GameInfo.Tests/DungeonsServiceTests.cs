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
    public class DungeonsServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoDungeons_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddDungeon_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var dungeonToAdd = new AddDungeonInputModel()
                { Name = "Added dungeon" };

                var service = new DungeonsService(context, null);
                service.Add(dungeonToAdd);

                var expectedDungeon = new Dungeon()
                { Name = dungeonToAdd.Name };

                Assert.NotEmpty(context.Dungeons);
                Assert.Equal(expectedDungeon.Name, context.Dungeons.First().Name);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithDungeons")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);

                var dungeons = new List<Dungeon>
                {
                    new Dungeon() {Name = "1"},
                    new Dungeon() {Name = "2"},
                    new Dungeon() {Name = "3"}
                };

                context.Dungeons.AddRange(dungeons);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoDungeons_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoDungeons_Db_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithDungeon_ReturnsDungeon()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForById_WithDungeon")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);

                var dungeonToAdd = new Dungeon()
                {
                    Name = "Dungeon"
                };

                context.Dungeons.Add(dungeonToAdd);
                context.SaveChanges();

                var dungeonFromDb = service.ById(1);

                Assert.Equal(dungeonToAdd.Name, dungeonFromDb.Name);
            }
        }                

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoDungeons_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesDungeon()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithDungeon_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Dungeons.Add(new Dungeon() { Name = "ToDelete" });
                context.SaveChanges();
            
                var service = new DungeonsService(context, null);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Dungeons.Count());
            }
        }

        [Fact]
        public void AddBossToDungeon_AddsData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Dungeons_Db_ForAddBoss")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Dungeons.Add(new Dungeon() { Name = "DungeonForBoss" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);

                var npc = new NPC() { Name = "NPC" };
                var model = new AddBossToDungeonInputModel()
                {
                    DungeonId = 1
                };

                var result = service.AddBossToDungeon(model, npc);
                Assert.True(result);
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void AddItemToDungeon_AddsData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Dungeons_Db_ForAddItem")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Dungeons.Add(new Dungeon() { Name = "DungeonForItem" });
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new DungeonsService(context, null);

                var item = new Item() { Name = "Item", AcquiredFrom = "None", Usage = "None" };
                var model = new AddItemToDungeonInputModel()
                {
                    DungeonId = 1
                };

                var result = service.AddItemToDungeon(model, item);
                Assert.True(result);
            }
        }

        [Fact]
        public async Task RemoveBoss_RemovesDataProperly()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Dungeons_Db_ForRemoveBoss")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var dungeonName = "DungeonBossRemove";
                var npc = new NPC() { Name = "Boss" };
                var dungeon = new Dungeon()
                {
                    Name = dungeonName,
                    Bosses = new List<NPC>() { npc }
                };

                context.Dungeons.Add(dungeon);
                context.SaveChanges();

                var service = new DungeonsService(context, null);
                
                await service.RemoveBoss(dungeon, npc);

                var dungeonFromDb = context.Dungeons.Include(x => x.Bosses).FirstOrDefault(x => x.Name == dungeonName);

                Assert.DoesNotContain(npc, dungeonFromDb.Bosses);
            }
        }

        [Fact]
        public async Task RemoveItem_RemovesDataProperly()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Dungeons_Db_ForRemoveItem")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var dungeonName = "DungeonItemRemove";
                var item = new Item() { Name = "Item" };
                var dungeon = new Dungeon()
                {
                    Name = dungeonName,
                    Rewards = new List<Item>() { item }
                };

                context.Dungeons.Add(dungeon);
                context.SaveChanges();

                var service = new DungeonsService(context, null);

                await service.RemoveItem(dungeon, item);

                var dungeonFromDb = context.Dungeons.Include(x => x.Rewards).FirstOrDefault(x => x.Name == dungeonName);

                Assert.DoesNotContain(item, dungeonFromDb.Rewards);
            }
        }
    }
}