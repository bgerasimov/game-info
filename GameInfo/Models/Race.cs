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
            this.Professions = new List<RaceProfession>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<RaceProfession> Professions { get; set; }
    }
}
