using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Details(int id)
        {
            var dungeon = _dungeonsService.ById(id);

            if (dungeon == null)
            {
                return Redirect("/Dungeons");
            }

            var viewModel = new DungeonDetailsViewModel
            {
                Id = dungeon.Id,
                Name = dungeon.Name,
                Bosses = dungeon.Bosses.Select(x => new NPCsAllViewModel { Id = x.Id, Name = x.Name }).ToList(),
                ItemRewards = dungeon.Rewards.Select(x => new ItemsAllViewModel { Id = x.Id, Name = x.Name }).ToList()
            };
                        
            viewModel.AchievementRewardName = _achievementsService.ById(dungeon.AchievementRewardId)?.Name;
            viewModel.AchievementRewardId = _achievementsService.ById(dungeon.AchievementRewardId)?.Id;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _dungeonsService.Delete(id);

            if (success)
            {
                return Redirect("/Dungeons/DeleteSuccess");
            }

            return Redirect("/Dungeons/Index");
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}