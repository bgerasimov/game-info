using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddBossToDungeonInputModel
    {
        public int DungeonId { get; set; }

        public string DungeonName { get; set; }

        public IList<NPCsAllViewModel> NPCs { get; set; }

        public string BossName { get; set; }
    }
}
