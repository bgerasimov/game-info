using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IRacesService
    {
        IList<RacesAllViewModel> All();
        
        void Add(AddRaceInputModel model);
        
        //Quest ById(int id);
        //
        //Quest ByName(string title);
        //
        //bool Delete(int id);
    }
}
