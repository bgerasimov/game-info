using GameInfo.Data;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class QuestsService : IQuestsService
    {
        private readonly GameInfoContext _db;

        public QuestsService(GameInfoContext db)
        {
            _db = db;
        }

        public IList<QuestsAllViewModel> All()
        {
            var quests = this._db.Quests?
               .Select(x => new QuestsAllViewModel
               {
                   Id = x.Id,
                   QuestTitle = x.Title,
                   QuestGiver = x.QuestGiver.Name,
                   QuestGiverId = x.QuestGiverId
               })
               .ToList();

            return quests;
        }
    }
}
