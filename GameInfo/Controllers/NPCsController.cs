using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class NPCsController : Controller
    {
        private readonly INPCsService _NPCsService;
        private readonly AuthorizerService _authorizerService;
        private readonly IItemsService _itemsService;

        public NPCsController(INPCsService NPCsService,
            IItemsService itemsService,
            AuthorizerService authorizerService)
        {
            _NPCsService = NPCsService;
            _authorizerService = authorizerService;
            _itemsService = itemsService;
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

            return Redirect("/Account/Login");
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

            return Redirect("/NPCs");
        }

        public IActionResult Details(int id)
        {
            var npc = _NPCsService.ById(id);

            if (npc == null)
            {
                return Redirect("/NPCs");
            }

            return View(npc);
        }

        public async Task<IActionResult> AddItem(int id)
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user == null)
            {
                return Redirect("/Account/Login");
            }

            var npc = _NPCsService.ById(id);

            if (npc == null)
            {
                return Redirect("/NPCs");
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _NPCsService.Delete(id);

            if (success)
            {
                return Redirect("/NPCs/DeleteSuccess");
            }

            return Redirect("/NPCs/Index");
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}