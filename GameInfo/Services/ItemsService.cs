using GameInfo.Data;
using GameInfo.Models;
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
    }
}
