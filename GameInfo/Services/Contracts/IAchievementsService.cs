using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IAchievementsService
    {
        IList<AchievementsAllViewModel> All();

        void Add(AddAchievementInputModel model);

        Achievement ByName(string name);

        Achievement ById(int? id);
    }
}