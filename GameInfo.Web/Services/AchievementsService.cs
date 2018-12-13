using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class AchievementsService : IAchievementsService
    {
        private readonly GameInfoContext _db;
        private readonly IItemsService _itemsService;

        public AchievementsService(GameInfoContext db, IItemsService itemsService)
        {
            _db = db;
            _itemsService = itemsService;
        }

        public void Add(AddAchievementInputModel model)
        {
            var achievement = new Achievement
            {
                Name = model.Name,
                AcquisitionConditions = model.AcquisitionConditions
            };

            this._db.Achievements.Add(achievement);
            this._db.SaveChanges();
        }

        public bool AddItemToAchievement(AddItemToAchievementInputModel model)
        {
            var achievement = this.ById(model.AchievementId);

            if (achievement == null)
            {
                return false;
            }

            var itemToAdd = _itemsService.ByName(model.ItemName);

            achievement.Rewards.Add(itemToAdd);
            _db.SaveChanges();

            return true;
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

        public Achievement ById(int? id)
        {
            var achievement = _db.Achievements.Include(x => x.Rewards).FirstOrDefault(x => x.Id == id);

            return achievement;
        }

        public Achievement ByName(string name)
        {
            var achievement = _db.Achievements.FirstOrDefault(x => x.Name == name);

            return achievement;
        }

        public bool Delete(int id)
        {
            var achievement = this.ById(id);

            if (achievement != null)
            {
                _db.Achievements.Remove(achievement);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
