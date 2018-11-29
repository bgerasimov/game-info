using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class NPCsService : INPCsService
    {
        private readonly GameInfoContext _db;

        public NPCsService(GameInfoContext db)
        {
            _db = db;
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
    }
}
