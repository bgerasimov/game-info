using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class Quest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string QuestText { get; set; }

        public string CompletionCondition { get; set; }

        public NPC QuestGiver { get; set; }
        public int? QuestGiverId { get; set; }
    }
}
