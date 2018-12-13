using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class AchievementsController : Controller
    {
        private const string Achievements_Root_Path = "/Achievements";
        private readonly IAchievementsService _achievementsService;
        private readonly IItemsService _itemsService;
        private readonly AuthorizerService _authorizerService;

        public AchievementsController(IAchievementsService achievementsService,
            IItemsService itemsService,
            AuthorizerService authorizerService)
        {
            _achievementsService = achievementsService;
            _itemsService = itemsService;
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

            return Redirect(GlobalConstants.Default_Login_Page);
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

            return Redirect(Achievements_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var achievement = _achievementsService.ById(id);

            if (achievement == null)
            {
                return Redirect(Achievements_Root_Path);
            }

            return View(achievement);
        }

        public async Task<IActionResult> AddReward(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect(GlobalConstants.Default_Login_Page);
            }

            var achievement = _achievementsService.ById(id);

            if (achievement == null)
            {
                return Redirect(Achievements_Root_Path);
            }

            var model = new AddItemToAchievementInputModel
            {
                AchievementId = id,
                AchievementName = achievement.Name,
                Items = _itemsService.All()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReward(AddItemToAchievementInputModel model)
        {
            var success = _achievementsService.AddItemToAchievement(model);

            if (success)
            {
                return RedirectToAction("Details", new { id = model.AchievementId });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _achievementsService.Delete(id);

            if (success)
            {
                return Redirect("/Achievements/DeleteSuccess");
            }

            return Redirect(Achievements_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}