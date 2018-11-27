using GameInfo.Data;
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
