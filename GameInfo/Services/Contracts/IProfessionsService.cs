using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Contracts
{
    public interface IProfessionsService
    {
        IList<ProfessionsAllViewModel> All();

        void Add(AddProfessionInputModel model);

        Profession ById(int id);

        Profession ByName(string name);
    }
}
