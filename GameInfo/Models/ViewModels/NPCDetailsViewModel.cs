using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class NPCDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<QuestsAllViewModel> Quests { get; set; }

        public List<ItemsAllViewModel> Loot { get; set; }

        public int QuestId { get; set; }

        public int ItemId { get; set; }
    }
}
