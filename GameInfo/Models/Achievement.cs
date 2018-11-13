using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class Achievement
    {
        public Achievement()
        {
            this.Rewards = new HashSet<Item>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string AcquisitionConditions { get; set; }

        public ICollection<Item> Rewards { get; set; }
    }
}
