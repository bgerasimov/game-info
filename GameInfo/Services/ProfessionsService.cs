using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.Enums;
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
    public class ProfessionsService : IProfessionsService
    {
        private readonly GameInfoContext _db;

        public ProfessionsService(GameInfoContext db)
        {
            _db = db;
        }

        public void Add(AddProfessionInputModel model)
        {
            var profession = new Profession
            {
                Name = model.Name,
                ClassRole = (ClassRole)Enum.Parse(typeof(ClassRole), model.ClassRole),
                CombatType = (CombatType)Enum.Parse(typeof(CombatType), model.CombatType),
                UsableWeapon = (WeaponType)Enum.Parse(typeof(WeaponType), model.UsableWeaponType)                
            };

            this._db.Professions.Add(profession);
            this._db.SaveChanges();
        }

        public IList<ProfessionsAllViewModel> All()
        {
            var professions = this._db.Professions?
               .Select(x => new ProfessionsAllViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   ClassRole = x.ClassRole.ToString()
               })
               .ToList();

            return professions;
        }

        public Profession ById(int id)
        {
            var profession = _db.Professions.Include(x => x.Races).FirstOrDefault(x => x.Id == id);

            return profession;
        }

        public Profession ByName(string name)
        {
            var profession = _db.Professions.FirstOrDefault(x => x.Name == name);

            return profession;
        }

        public async Task RemoveRace(Profession profession, Race race)
        {
            if (profession.Races.Any(x => x.RaceId == race.Id))
            {
                var raceToRemove = profession.Races.FirstOrDefault(x => x.RaceId == race.Id);

                profession.Races.Remove(raceToRemove);
                await _db.SaveChangesAsync();
            }
        }

        public bool Delete(int id)
        {
            var profession = this.ById(id);

            if (profession != null)
            {
                _db.Professions.Remove(profession);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
