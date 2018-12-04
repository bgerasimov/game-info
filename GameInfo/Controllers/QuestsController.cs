﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class QuestsController : Controller
    {
        private readonly IQuestsService _questsService;
        private readonly AuthorizerService _authorizerService;
        private readonly INPCsService _NPCsService;

        public QuestsController(IQuestsService questsService, AuthorizerService authorizerService, INPCsService NPCsService)
        {
            _questsService = questsService;
            _authorizerService = authorizerService;
            _NPCsService = NPCsService;
        }

        public IActionResult Index()
        {
            var model = _questsService.All()?.ToList();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user != null)
            {
                var model = new AddQuestInputModel
                {
                    NPCs = _NPCsService.All()?.ToList()
                };
                
                return View(model);
            }

            return Redirect("/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddQuestInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _questsService.Add(inputModel);

            return Redirect("/Quests");
        }
    }
}