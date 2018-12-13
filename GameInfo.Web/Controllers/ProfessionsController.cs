using GameInfo.Models.Enums;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Controllers
{
    public class ProfessionsController : Controller
    {
        private const string Professions_Root_Path = "/Professions";
        private readonly IProfessionsService _professionsService;
        private readonly IRacesService _racesService;
        private readonly AuthorizerService _authorizerService;

        public ProfessionsController(IProfessionsService professionsService, IRacesService racesService, AuthorizerService authorizerService)
        {
            _professionsService = professionsService;
            _authorizerService = authorizerService;
            _racesService = racesService;
        }

        public IActionResult Index()
        {
            var model = _professionsService.All()?.ToList();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user != null)
            {
                var model = new AddProfessionInputModel
                {
                    ClassRoles = Enum.GetNames(typeof(ClassRole)).ToList(),
                    CombatTypes = Enum.GetNames(typeof(CombatType)).ToList(),
                    WeaponTypes = Enum.GetNames(typeof(WeaponType)).ToList()
                };

                return View(model);
            }

            return Redirect(GlobalConstants.Default_Login_Page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddProfessionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new AddProfessionInputModel
                {
                    ClassRoles = Enum.GetNames(typeof(ClassRole)).ToList(),
                    CombatTypes = Enum.GetNames(typeof(CombatType)).ToList(),
                    WeaponTypes = Enum.GetNames(typeof(WeaponType)).ToList()
                };

                return View(model);
            }

            _professionsService.Add(inputModel);

            return Redirect(Professions_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var profession = _professionsService.ById(id);

            if (profession == null)
            {
                return Redirect(Professions_Root_Path);
            }

            var viewModel = new ProfessionDetailsViewModel
            {
                Id = profession.Id,
                Name = profession.Name,
                UsableWeapon = profession.UsableWeapon.ToString(),
                CombatType = profession.CombatType.ToString(),
                ClassRole = profession.ClassRole.ToString()
            };

            if (profession.Races.Any())
            {
                foreach (var raceProfession in profession.Races)
                {
                    var race = _racesService.ById(raceProfession.RaceId);

                    viewModel.Races.Add(new RacesAllViewModel { Id = race.Id, Name = race.Name });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRace(ProfessionDetailsViewModel model)
        {
            var profession = _professionsService.ById(model.Id);

            var race = _racesService.ById(model.RaceId);

            if (race == null || profession == null)
            {
                return Redirect(Professions_Root_Path);
            }

            await _professionsService.RemoveRace(profession, race);

            return Redirect("/Professions/Details/" + profession.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _professionsService.Delete(id);

            if (success)
            {
                return Redirect("/Professions/DeleteSuccess");
            }

            return Redirect(Professions_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}
