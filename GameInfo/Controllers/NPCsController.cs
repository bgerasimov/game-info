using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class NPCsController : Controller
    {
        private readonly INPCsService _NPCsService;

        public NPCsController(INPCsService NPCsService)
        {
            _NPCsService = NPCsService;
        }

        public IActionResult Index()
        {
            var model = _NPCsService.All()?.ToList();

            return View(model);
        }
    }
}