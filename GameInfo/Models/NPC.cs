using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class NPC
    {
        public NPC()
        {
            this.Quests = new List<Quest>();
            this.Loot = new List<Item>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Quest> Quests { get; set; }

        public List<Item> Loot { get; set; }

        public Dungeon Dungeon { get; set; }
        public int? DungeonId { get; set; }
    }
}
