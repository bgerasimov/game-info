using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class RacesService : IRacesService
    {
        private readonly GameInfoContext _db;

        public RacesService(GameInfoContext db)
        {
            _db = db;
        }

        public void Add(AddRaceInputModel model)
        {
            var race = new Race
            {
                Name = model.Name,
                Description = model.Description
            };

            this._db.Races.Add(race);
            this._db.SaveChanges();
        }

        public IList<RacesAllViewModel> All()
        {
            var races = this._db.Races?
               .Select(x => new RacesAllViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description
               })
               .ToList();

            return races;
        }
    }
}
