using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddProfessionToRaceInputModel
    {
        public int RaceId { get; set; }

        public string RaceName { get; set; }

        public IList<ProfessionsAllViewModel> Professions { get; set; }

        public string ProfessionName { get; set; }
    }
}
