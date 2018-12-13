using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class NPCsController : Controller
    {
        private const string NPCs_Root_Path = "/NPCs";
        private readonly INPCsService _NPCsService;
        private readonly AuthorizerService _authorizerService;
        private readonly IItemsService _itemsService;
        private readonly IQuestsService _questsService;

        public NPCsController(INPCsService NPCsService,
            IItemsService itemsService,
            IQuestsService questsService,
            AuthorizerService authorizerService)
        {
            _NPCsService = NPCsService;
            _authorizerService = authorizerService;
            _itemsService = itemsService;
            _questsService = questsService;
        }

        public IActionResult Index()
        {
            var model = _NPCsService.All()?.ToList();

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
        public IActionResult Add(AddNPCInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _NPCsService.Add(inputModel);

            return Redirect(NPCs_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var npc = _NPCsService.ById(id);

            if (npc == null)
            {
                return Redirect(NPCs_Root_Path);
            }

            var viewModel = new NPCDetailsViewModel
            {
                Id = npc.Id,
                Name = npc.Name,
                Loot = npc.Loot.Select(x => new ItemsAllViewModel { Id = x.Id, Name = x.Name }).ToList(),
                Quests = npc.Quests.Select(x => new QuestsAllViewModel { Id = x.Id, QuestTitle = x.Title }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddItem(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect(GlobalConstants.Default_Login_Page);
            }

            var npc = _NPCsService.ById(id);

            if (npc == null)
            {
                return Redirect(NPCs_Root_Path);
            }

            var model = new AddItemToNPCInputModel
            {
                NPCId = id,
                NPCName = npc.Name,
                Items = _itemsService.All()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(AddItemToNPCInputModel model)
        {
            var success = _NPCsService.AddItemToNPC(model);

            if (success)
            {
                return RedirectToAction("Details", new { id = model.NPCId });
            }

            return View(model);
        }

        public async Task<IActionResult> AddQuest(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect(GlobalConstants.Default_Login_Page);
            }

            var npc = _NPCsService.ById(id);

            if (npc == null)
            {
                return Redirect(NPCs_Root_Path);
            }

            var model = new AddQuestToNPCInputModel
            {
                NPCId = id,
                NPCName = npc.Name,
                Quests = _questsService.All()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuest(AddQuestToNPCInputModel model)
        {
            var questToAdd = _questsService.ByName(model.QuestTitle);
            var success = _NPCsService.AddQuestToNPC(model, questToAdd);

            if (success)
            {
                return RedirectToAction("Details", new { id = model.NPCId });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveQuest(NPCDetailsViewModel model)
        {
            var npc = _NPCsService.ById(model.Id);

            var quest = _questsService.ById(model.QuestId);

            if (quest == null || npc == null)
            {
                return Redirect(NPCs_Root_Path);
            }
            
            await _NPCsService.RemoveQuest(npc, quest);

            return RedirectToAction("Details", new { id = npc.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveItem(NPCDetailsViewModel model)
        {
            var npc = _NPCsService.ById(model.Id);

            var item = _itemsService.ById(model.ItemId);

            if (item == null || npc == null)
            {
                return Redirect(NPCs_Root_Path);
            }

            await _NPCsService.RemoveItem(npc, item);

            return RedirectToAction("Details", new { id = npc.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _NPCsService.Delete(id);

            if (success)
            {
                return Redirect("/NPCs/DeleteSuccess");
            }

            return Redirect(NPCs_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}