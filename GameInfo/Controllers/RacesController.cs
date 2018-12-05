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
    public class RacesController : Controller
    {
        private readonly IRacesService _racesService;
        private readonly AuthorizerService _authorizerService;

        public RacesController(IRacesService racesService, AuthorizerService authorizerService)
        {
            _racesService = racesService;
            _authorizerService = authorizerService;
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

            return Redirect("/Account/Login");
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

            return Redirect("/Races");
        }
    }
}