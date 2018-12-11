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
    public class AchievementsController : Controller
    {
        private readonly IAchievementsService _achievementsService;
        private readonly AuthorizerService _authorizerService;

        public AchievementsController(IAchievementsService achievementsService, AuthorizerService authorizerService)
        {
            _achievementsService = achievementsService;
            _authorizerService = authorizerService;
        }

        public IActionResult Index()
        {
            var model = _achievementsService.All()?.ToList();

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
        public IActionResult Add(AddAchievementInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _achievementsService.Add(inputModel);

            return Redirect("/Achievements");
        }
    }
}