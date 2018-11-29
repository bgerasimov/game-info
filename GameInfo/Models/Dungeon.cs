using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class Dungeon
    {
        public Dungeon()
        {
            this.Bosses = new List<NPC>();
            this.Rewards = new List<Item>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Achievement AchievementReward { get; set; }

        public List<NPC> Bosses { get; set; }

        public List<Item> Rewards { get; set; }
    }
}
