using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class RacesController : Controller
    {
        private const string Races_Root_Path = "/Races";
        private readonly IRacesService _racesService;
        private readonly IProfessionsService _professionsService;
        private readonly AuthorizerService _authorizerService;

        public RacesController(IRacesService racesService, IProfessionsService professionsService, AuthorizerService authorizerService)
        {
            _racesService = racesService;
            _authorizerService = authorizerService;
            _professionsService = professionsService;
        }

        public IActionResult Index()
        {
            var model = _racesService.All()?.ToList();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user != null)
            {
                return View();
            }

            return Redirect(GlobalConstants.Default_Login_Page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddRaceInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _racesService.Add(inputModel);

            return Redirect(Races_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var race = _racesService.ById(id);

            if (race == null)
            {
                return Redirect(Races_Root_Path);
            }

            var model = new RaceDetailsViewModel
            {
                Id = race.Id,
                Name = race.Name,
                Description = race.Description                
            };

            model.Professions = race.Professions?
                    .Select(x => new ProfessionsAllViewModel
                    {
                        Id = x.ProfessionId,
                        Name = _professionsService.ById(x.ProfessionId).Name
                    }).ToList();

            return View(model);
        }

        public async Task<IActionResult> AddProfession(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect(GlobalConstants.Default_Login_Page);
            }

            var race = _racesService.ById(id);

            if (race == null)
            {
                return Redirect(Races_Root_Path);
            }

            var model = new AddProfessionToRaceInputModel
            {
                RaceId = id,
                RaceName = race.Name,
                Professions = _professionsService.All()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProfession(AddProfessionToRaceInputModel model)
        {
            var success = _racesService.AddProfessionToRace(model);

            if (success)
            {
                return RedirectToAction("Details", new { id = model.RaceId });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _racesService.Delete(id);

            if (success)
            {
                return Redirect("/Races/DeleteSuccess");
            }

            return Redirect(Races_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}