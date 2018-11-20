using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Controllers
{
    public class GuidesController : Controller
    {
        private readonly IGuidesService _guidesService;

        public GuidesController(IGuidesService guidesService)
        {
            _guidesService = guidesService;
        }

        public IActionResult Index()
        {
            var model = _guidesService.All().ToList();

            return View(model);
        }
    }
}
