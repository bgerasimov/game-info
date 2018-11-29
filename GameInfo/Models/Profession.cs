using GameInfo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class Profession
    {
        public Profession()
        {
            this.Races = new List<RaceProfession>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public WeaponType UsableWeapon { get; set; }

        public CombatType CombatType { get; set; }

        public ClassRole ClassRole { get; set; }

        public virtual List<RaceProfession> Races { get; set; }
    }
}
