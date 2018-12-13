using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddQuestToNPCInputModel
    {
        public int NPCId { get; set; }

        public string NPCName { get; set; }

        public IList<QuestsAllViewModel> Quests { get; set; }

        public string QuestTitle { get; set; }
    }
}
