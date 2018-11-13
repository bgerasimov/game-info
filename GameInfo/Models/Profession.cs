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
            this.UsableWeapons = new HashSet<WeaponType>();
            this.CombatTypes = new HashSet<CombatType>();
            this.ClassRoles = new HashSet<ClassRole>();
            this.AvailableTo = new HashSet<Race>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<WeaponType> UsableWeapons { get; set; }

        public ICollection<CombatType> CombatTypes { get; set; }

        public ICollection<ClassRole> ClassRoles { get; set; }

        public ICollection<Race> AvailableTo { get; set; }
    }
}
