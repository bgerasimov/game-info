using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class QuestsController : Controller
    {
        private readonly IQuestsService _questsService;

        public QuestsController(IQuestsService questsService)
        {
            _questsService = questsService;
        }

        public IActionResult Index()
        {
            var model = _questsService.All()?.ToList();

            return View(model);
        }
    }
}