using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IGuidesService
    {
        IList<GuidesAllViewModel> All();

        void Add(AddGuideInputModel model, GameInfoUser currentUser);

        Guide ById(int id);

        bool Delete(int id);
    }
}
