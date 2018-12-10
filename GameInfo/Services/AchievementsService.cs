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
    public class AchievementsService : IAchievementsService
    {
        private readonly GameInfoContext _db;

        public AchievementsService(GameInfoContext db)
        {
            _db = db;
        }

        public IList<AchievementsAllViewModel> All()
        {
            var achievements = this._db.Achievements?
               .Select(x => new AchievementsAllViewModel
               {
                   Id = x.Id,
                   Name = x.Name
               })
               .ToList();

            return achievements;
        }

        public Achievement ByName(string name)
        {
            var achievement = _db.Achievements.FirstOrDefault(x => x.Name == name);

            return achievement;
        }
    }
}
