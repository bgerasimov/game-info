using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class RaceDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProfessionsAllViewModel> Professions { get; set; }

        public int ProfessionId { get; set; }
    }
}
