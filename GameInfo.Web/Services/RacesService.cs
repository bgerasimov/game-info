using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class RacesService : IRacesService
    {
        private readonly GameInfoContext _db;
        private readonly IProfessionsService _professionsService;

        public RacesService(GameInfoContext db, IProfessionsService professionsService)
        {
            _db = db;
            _professionsService = professionsService;
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

        public bool AddProfessionToRace(AddProfessionToRaceInputModel model)
        {
            var race = this.ById(model.RaceId);

            if (race == null)
            {
                return false;
            }

            var professionToAdd = _professionsService.ByName(model.ProfessionName);

            race.Professions.Add(new RaceProfession {
                Race = race,
                RaceId = race.Id,
                Profession = professionToAdd,
                ProfessionId = professionToAdd.Id
            });
            _db.SaveChanges();

            return true;
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

        public Race ById(int id)
        {
            var race = _db.Races.Include(x => x.Professions).FirstOrDefault(x => x.Id == id);

            return race;
        }

        public bool Delete(int id)
        {
            var race = this.ById(id);

            if (race != null)
            {
                _db.Races.Remove(race);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
