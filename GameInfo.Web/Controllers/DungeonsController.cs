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
    public class DungeonsController : Controller
    {
        private const string Dungeons_Root_Path = "/Dungeons";
        private readonly IDungeonsService _dungeonsService;
        private readonly IAchievementsService _achievementsService;
        private readonly IItemsService _itemsService;
        private readonly INPCsService _NPCsService;
        private readonly AuthorizerService _authorizerService;

        public DungeonsController(IDungeonsService dungeonsService,
            IAchievementsService achievementsService,
            IItemsService itemsService,
            INPCsService NPCsService,
            AuthorizerService authorizerService)
        {
            _dungeonsService = dungeonsService;
            _authorizerService = authorizerService;
            _achievementsService = achievementsService;
            _itemsService = itemsService;
            _NPCsService = NPCsService;
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

            return Redirect(GlobalConstants.Default_Login_Page);
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

            return Redirect(Dungeons_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var dungeon = _dungeonsService.ById(id);

            if (dungeon == null)
            {
                return Redirect(Dungeons_Root_Path);
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

        public async Task<IActionResult> AddBoss(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect(GlobalConstants.Default_Login_Page);
            }

            var dungeon = _dungeonsService.ById(id);

            if (dungeon == null)
            {
                return Redirect(Dungeons_Root_Path);
            }

            var model = new AddBossToDungeonInputModel
            {
                DungeonId = id,
                DungeonName = dungeon.Name,
                NPCs = _NPCsService.All()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBoss(AddBossToDungeonInputModel model)
        {
            var bossToAdd = _NPCsService.ByName(model.BossName);
            var success = _dungeonsService.AddBossToDungeon(model, bossToAdd);

            if (success)
            {
                return RedirectToAction("Details", new { id = model.DungeonId });
            }

            return View(model);
        }

        public async Task<IActionResult> AddReward(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect(GlobalConstants.Default_Login_Page);
            }

            var dungeon = _dungeonsService.ById(id);

            if (dungeon == null)
            {
                return Redirect(Dungeons_Root_Path);
            }

            var model = new AddItemToDungeonInputModel
            {
                DungeonId = id,
                DungeonName = dungeon.Name,
                Items = _itemsService.All()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReward(AddItemToDungeonInputModel model)
        {
            var itemToAdd = _itemsService.ByName(model.ItemName);
            var success = _dungeonsService.AddItemToDungeon(model, itemToAdd);
            
            if (success)
            {
                return RedirectToAction("Details", new { id = model.DungeonId });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveBoss(DungeonDetailsViewModel model)
        {
            var dungeon = _dungeonsService.ById(model.Id);

            var boss = _NPCsService.ById(model.BossId);

            if (boss == null || dungeon == null)
            {
                return Redirect(Dungeons_Root_Path);
            }

            await _dungeonsService.RemoveBoss(dungeon, boss);

            return RedirectToAction("Details", new { id = dungeon.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveItem(DungeonDetailsViewModel model)
        {
            var dungeon = _dungeonsService.ById(model.Id);

            var item = _itemsService.ById(model.ItemId);

            if (item == null || dungeon == null)
            {
                return Redirect(Dungeons_Root_Path);
            }

            await _dungeonsService.RemoveItem(dungeon, item);

            return RedirectToAction("Details", new { id = dungeon.Id });
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

            return Redirect(Dungeons_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}