using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IItemsService
    {
        IList<ItemsAllViewModel> All();

        void Add(AddItemInputModel model);

        Item ById(int id);

        Item ByName(string name);

        bool Delete(int id);
    }
}
