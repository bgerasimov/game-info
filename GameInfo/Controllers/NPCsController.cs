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
    public class NPCsController : Controller
    {
        private readonly INPCsService _NPCsService;
        private readonly AuthorizerService _authorizerService;

        public NPCsController(INPCsService NPCsService, AuthorizerService authorizerService)
        {
            _NPCsService = NPCsService;
            _authorizerService = authorizerService;
        }

        public IActionResult Index()
        {
            var model = _NPCsService.All()?.ToList();

            return View(model);
        }

        public IActionResult Add()
        {
            var user = _authorizerService.Authorize(HttpContext);

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
    }
}