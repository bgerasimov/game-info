using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddDungeonInputModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public string AchievementReward { get; set; }

        public IList<AchievementsAllViewModel> Achievements { get; set; }
    }
}
