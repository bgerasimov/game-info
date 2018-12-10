using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class DungeonsController : Controller
    {
        private readonly IDungeonsService _dungeonsService;
        private readonly IAchievementsService _achievementsService;
        private readonly AuthorizerService _authorizerService;

        public DungeonsController(IDungeonsService dungeonsService,
            IAchievementsService achievementsService,
            AuthorizerService authorizerService)
        {
            _dungeonsService = dungeonsService;
            _authorizerService = authorizerService;
            _achievementsService = achievementsService;
        }

        public IActionResult Index()
        {
            var model = _dungeonsService.All()?.ToList();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user != null)
            {
                var model = new AddDungeonInputModel
                {
                    Achievements = _achievementsService.All()
                };

                return View(model);
            }

            return Redirect("/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddDungeonInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new AddDungeonInputModel
                {
                    Achievements = _achievementsService.All()
                };

                return View(model);
            }

            _dungeonsService.Add(inputModel);

            return Redirect("/Dungeons");
        }
    }
}