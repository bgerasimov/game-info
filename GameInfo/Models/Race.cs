using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class Race
    {
        public Race()
        {
            this.AvailableProfessions = new HashSet<Profession>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Profession> AvailableProfessions { get; set; }
    }
}
