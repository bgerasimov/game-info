using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class DungeonDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AchievementRewardName { get; set; }

        public int? AchievementRewardId { get; set; }

        public List<NPCsAllViewModel> Bosses { get; set; }

        public List<ItemsAllViewModel> ItemRewards { get; set; }

        public int BossId { get; set; }

        public int ItemId { get; set; }
    }
}
