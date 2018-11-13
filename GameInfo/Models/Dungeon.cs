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
            this.Rewards = new HashSet<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<NPC> Bosses { get; set; }

        public ICollection<string> Rewards { get; set; }
    }
}
