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
    public class ItemsServiceTests
    {
        [Fact]
        public void All_WithNoData_ReturnsNoData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoItems_Db")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);
                Assert.Equal(0, service.All().Count);
            }
        }

        [Fact]
        public void Add_SavesToDatabase()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "AddItem_ToDb")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var itemToAdd = new AddItemInputModel()
                { Name = "Item", AcquiredFrom = "How to get", Usage = "None" };

                var service = new ItemsService(context);
                service.Add(itemToAdd);

                var expectedItem = new Item()
                { Name = itemToAdd.Name, AcquiredFrom = itemToAdd.AcquiredFrom, Usage = itemToAdd.Usage };

                var itemFromDb = context.Items.First();

                Assert.NotEmpty(context.Items);
                Assert.Equal(expectedItem.Name, itemFromDb.Name);
                Assert.Equal(expectedItem.AcquiredFrom, itemFromDb.AcquiredFrom);
                Assert.Equal(expectedItem.Usage, itemFromDb.Usage);
            }
        }

        [Fact]
        public void All_WithData_ReturnsSameData()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithItems")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);

                var items = new List<Item>
                {
                    new Item() {Name = "1", AcquiredFrom="1", Usage = "1"},
                    new Item() {Name = "2", AcquiredFrom="2", Usage = "2"},
                    new Item() {Name = "3", AcquiredFrom="3", Usage = "3"}
                };

                context.Items.AddRange(items);
                context.SaveChanges();

                Assert.Equal(3, service.All().Count);
            }
        }

        [Fact]
        public void ById_WithNoItems_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoItems_Db_ForById")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);
                Assert.Null(service.ById(1));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void ById_WithItem_ReturnsItem()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForById_WithItem")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);

                var itemToAdd = new Item()
                {
                    Name = "Item",
                    AcquiredFrom = "Drop",
                    Usage = "None"
                };

                context.Items.Add(itemToAdd);
                context.SaveChanges();

                var itemFromDb = service.ById(1);

                Assert.Equal(itemToAdd.Name, itemFromDb.Name);
                Assert.Equal(itemToAdd.AcquiredFrom, itemFromDb.AcquiredFrom);
            }
        }

        [Fact]
        public void ByName_WithNoItems_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoItems_Db_ForByName")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);
                Assert.Null(service.ByName("Non-existing"));
            }
        }

        [Fact]
        public void ByName_WithItem_ReturnsItem()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_ForByName_WithItem")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);

                var itemName = "Item Name";

                var item = new Item()
                {
                    Name = itemName,
                    AcquiredFrom = "Random",
                    Usage = "None"
                };

                context.Items.Add(item);
                context.SaveChanges();

                var itemFromDb = service.ByName(itemName);

                Assert.Equal(item.Name, itemFromDb.Name);
                Assert.Equal(item.AcquiredFrom, itemFromDb.AcquiredFrom);
                Assert.Equal(item.Usage, itemFromDb.Usage);
            }
        }

        [Fact]
        public void Delete_NoData_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "NoItems_Db_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);
                Assert.False(service.Delete(2));
            }
        }

        //Does not succeed when tested along all the other tests
        [Fact]
        public void Delete_WithData_DeletesItem()
        {
            var options = new DbContextOptionsBuilder<GameInfoContext>()
                .UseInMemoryDatabase(databaseName: "Db_WithItems_ForDelete")
                .Options;

            using (var context = new GameInfoContext(options))
            {
                context.Items.Add(new Item() { Name = "ToDelete", AcquiredFrom = "None" , Usage= "None"});
                context.SaveChanges();
            }

            using (var context = new GameInfoContext(options))
            {
                var service = new ItemsService(context);
                var result = service.Delete(1);

                Assert.True(result);
                Assert.Equal(0, context.Items.Count());
            }
        }
    }
}
