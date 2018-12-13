using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface INPCsService
    {
        IList<NPCsAllViewModel> All();

        void Add(AddNPCInputModel model);

        NPC ById(int id);

        NPC ByName(string name);

        bool Delete(int id);

        bool AddItemToNPC(AddItemToNPCInputModel model);

        bool AddQuestToNPC(AddQuestToNPCInputModel model, Quest questToAdd);

        Task RemoveQuest(NPC npc, Quest quest);

        Task RemoveItem(NPC npc, Item item);
    }
}
