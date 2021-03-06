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
    public class NPCsService : INPCsService
    {
        private readonly GameInfoContext _db;
        private readonly IItemsService _itemsService;        

        public NPCsService(GameInfoContext db, IItemsService itemsService)
        {
            _db = db;
            _itemsService = itemsService;
        }

        public void Add(AddNPCInputModel model)
        {
            var npc = new NPC
            {
                Name = model.Name
            };

            this._db.NPCs.Add(npc);
            this._db.SaveChanges();
        }

        public bool AddItemToNPC(AddItemToNPCInputModel model)
        {
            var npc = this.ById(model.NPCId);

            if (npc == null)
            {
                return false;
            }

            var itemToAdd = _itemsService.ByName(model.ItemName);

            npc.Loot.Add(itemToAdd);
            _db.SaveChanges();

            return true;
        }

        public bool AddQuestToNPC(AddQuestToNPCInputModel model, Quest questToAdd)
        {
            var npc = this.ById(model.NPCId);

            if (npc == null)
            {
                return false;
            }

            var quest = questToAdd;

            npc.Quests.Add(quest);
            _db.SaveChanges();

            return true;
        }

        public IList<NPCsAllViewModel> All()
        {
            var NPCs = this._db.NPCs?
               .Select(x => new NPCsAllViewModel
               {
                   Id = x.Id,
                   Name = x.Name
               })
               .ToList();

            return NPCs;
        }

        public NPC ById(int id)
        {
            var npc = _db.NPCs.Include(x => x.Loot).Include(x => x.Quests).FirstOrDefault(x => x.Id == id);
           
            return npc;
        }

        public NPC ByName(string name)
        {
            var npc = _db.NPCs.FirstOrDefault(x => x.Name == name);

            return npc;
        }

        public bool Delete(int id)
        {
            var npc = this.ById(id);

            if (npc != null)
            {
                _db.NPCs.Remove(npc);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task RemoveItem(NPC npc, Item item)
        {
            if (npc.Loot.Contains(item))
            {
                npc.Loot.Remove(item);
                await _db.SaveChangesAsync();
            }
        }

        public async Task RemoveQuest(NPC npc, Quest quest)
        {
            if (npc.Quests.Contains(quest))
            {
                npc.Quests.Remove(quest);
                await _db.SaveChangesAsync();
            }
        }
    }
}
