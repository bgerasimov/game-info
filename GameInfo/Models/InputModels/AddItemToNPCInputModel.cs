using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddItemToNPCInputModel
    {
        public int NPCId { get; set; }

        public string NPCName { get; set; }

        public IList<ItemsAllViewModel> Items { get; set; }

        public string ItemName { get; set; }
    }
}
