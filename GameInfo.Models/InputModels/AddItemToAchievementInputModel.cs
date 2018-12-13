using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddItemToAchievementInputModel
    {
        public int AchievementId { get; set; }

        public string AchievementName { get; set; }

        public IList<ItemsAllViewModel> Items { get; set; }

        public string ItemName { get; set; }
    }
}
