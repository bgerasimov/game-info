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
            this.Quests = new HashSet<Quest>();
            this.Loot = new HashSet<Item>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Quest> Quests { get; set; }

        public ICollection<Item> Loot { get; set; }
    }
}
