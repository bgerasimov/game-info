using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IDungeonsService
    {
        IList<DungeonsAllViewModel> All();

        void Add(AddDungeonInputModel model);

        Dungeon ById(int id);

        bool Delete(int id);

        bool AddBossToDungeon(AddBossToDungeonInputModel model, NPC NPCToAdd);

        bool AddItemToDungeon(AddItemToDungeonInputModel model, Item itemToAdd);

        Task RemoveBoss(Dungeon dungeon, NPC boss);

        Task RemoveItem(Dungeon dungeon, Item item);
    }
}
