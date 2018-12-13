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
    public class QuestsService : IQuestsService
    {
        private readonly GameInfoContext _db;
        private readonly INPCsService _npcsService;

        public QuestsService(GameInfoContext db, INPCsService npcsService)
        {
            _db = db;
            _npcsService = npcsService;
        }

        public void Add(AddQuestInputModel model)
        {
            var quest = new Quest
            {
                Title = model.Title,
                QuestText = model.QuestText,
                CompletionCondition = model.CompletionCondition
            };

            if (model.QuestGiver != null)
            {
                quest.QuestGiver = _npcsService.ByName(model.QuestGiver);
            }

            this._db.Quests.Add(quest);
            this._db.SaveChanges();
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

        public Quest ById(int id)
        {
            var quest = _db.Quests.Include(x => x.QuestGiver).FirstOrDefault(x => x.Id == id);

            return quest;
        }

        public Quest ByName(string title)
        {
            var quest = _db.Quests.FirstOrDefault(x => x.Title == title);

            return quest;
        }

        public bool Delete(int id)
        {
            var quest = this.ById(id);

            if (quest != null)
            {
                _db.Quests.Remove(quest);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
