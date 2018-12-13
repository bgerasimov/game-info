using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class QuestsAllViewModel
    {
        public int Id { get; set; }

        public string QuestTitle { get; set; }

        public string QuestGiver { get; set; }

        public int? QuestGiverId { get; set; }
    }
}
