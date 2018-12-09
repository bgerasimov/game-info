using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class ProfessionDetailsViewModel
    {
        public ProfessionDetailsViewModel()
        {
            this.Races = new List<RacesAllViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<RacesAllViewModel> Races { get; set; }

        public string UsableWeapon { get; set; }

        public string CombatType { get; set; }

        public string ClassRole { get; set; }

        public int RaceId { get; set; }
    }
}
