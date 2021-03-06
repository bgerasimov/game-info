﻿using GameInfo.Data;
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
    public class DungeonsService : IDungeonsService
    {
        private const string No_Achievement_Selected = "None";
        private readonly GameInfoContext _db;
        private readonly IAchievementsService _achievementsService;

        public DungeonsService(GameInfoContext db, IAchievementsService achievementsService)
        {
            _db = db;
            _achievementsService = achievementsService;
        }

        public void Add(AddDungeonInputModel model)
        {
            var dungeon = new Dungeon
            {
                Name = model.Name
            };

            if (model.AchievementReward != null && 
                model.AchievementReward != No_Achievement_Selected)
            {
                dungeon.AchievementReward = _achievementsService.ByName(model.AchievementReward);
            }

            this._db.Dungeons.Add(dungeon);
            this._db.SaveChanges();
        }

        public bool AddBossToDungeon(AddBossToDungeonInputModel model, NPC NPCToAdd)
        {
            var dungeon = this.ById(model.DungeonId);

            if (dungeon == null)
            {
                return false;
            }

            var npc = NPCToAdd;

            dungeon.Bosses.Add(npc);
            _db.SaveChanges();

            return true;
        }

        public bool AddItemToDungeon(AddItemToDungeonInputModel model, Item itemToAdd)
        {
            var dungeon = this.ById(model.DungeonId);

            if (dungeon == null)
            {
                return false;
            }

            var item = itemToAdd;

            dungeon.Rewards.Add(item);
            _db.SaveChanges();

            return true;
        }

        public IList<DungeonsAllViewModel> All()
        {
            var dungeons = this._db.Dungeons?
                .Include(x => x.Bosses)
               .Select(x => new DungeonsAllViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   BossCount = x.Bosses.Count
               })
               .ToList();

            return dungeons;
        }

        public Dungeon ById(int id)
        {
            var dungeon = _db.Dungeons.Include(x => x.Rewards).Include(x => x.Bosses).FirstOrDefault(x => x.Id == id);

            return dungeon;
        }

        public bool Delete(int id)
        {
            var dungeon = this.ById(id);

            if (dungeon != null)
            {
                _db.Dungeons.Remove(dungeon);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task RemoveItem(Dungeon dungeon, Item item)
        {
            if (dungeon.Rewards.Contains(item))
            {
                dungeon.Rewards.Remove(item);
                await _db.SaveChangesAsync();
            }
        }

        public async Task RemoveBoss(Dungeon dungeon, NPC boss)
        {
            if (dungeon.Bosses.Contains(boss))
            {
                dungeon.Bosses.Remove(boss);
                await _db.SaveChangesAsync();
            }
        }
    }
}
