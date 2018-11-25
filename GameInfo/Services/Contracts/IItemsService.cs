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
    }
}
