using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddProfessionInputModel
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Required]
        public string UsableWeaponType { get; set; }

        [Required]
        public string CombatType { get; set; }

        [Required]
        public string ClassRole { get; set; }

        public List<string> ClassRoles { get; set; }

        public List<string> WeaponTypes { get; set; }

        public List<string> CombatTypes { get; set; }
    }
}
