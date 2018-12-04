using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IQuestsService
    {
        IList<QuestsAllViewModel> All();

        void Add(AddQuestInputModel model);
        
        //Item ById(int id);

        Quest ByName(string title);

        //bool Delete(int id);
    }
}
