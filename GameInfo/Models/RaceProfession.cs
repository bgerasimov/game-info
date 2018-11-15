using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class RaceProfession
    {
        public Race Race { get; set; }
        public int RaceId { get; set; }

        public Profession Profession { get; set; }
        public int ProfessionId { get; set; }
    }
}
