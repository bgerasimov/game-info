using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class ItemsService : IItemsService
    {
        private readonly GameInfoContext _db;

        public ItemsService(GameInfoContext db)
        {
            _db = db;
        }

        public void Add(AddItemInputModel model)
        {
            var item = new Item
            {
                Name = model.Name,
                AcquiredFrom = model.AcquiredFrom,
                Usage = model.Usage
            };

            this._db.Items.Add(item);
            this._db.SaveChanges();
        }

        public IList<ItemsAllViewModel> All()
        {
            var items = this._db.Items?
               .Select(x => new ItemsAllViewModel
               {
                   Id = x.Id,
                   Name = x.Name
               })
               .ToList();

            return items;
        }

        public Item ById(int id)
        {
            var item = _db.Items.FirstOrDefault(x => x.Id == id);

            return item;
        }

        public Item ByName(string name)
        {
            var item = _db.Items.FirstOrDefault(x => x.Name == name);

            return item;
        }

        public bool Delete(int id)
        {
            var item = this.ById(id);

            if (item != null)
            {
                _db.Items.Remove(item);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
