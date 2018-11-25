using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameInfo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using GameInfo.Services.Contracts;

namespace GameInfo.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IGuidesService _guidesService;

        public HomeController(IGuidesService guidesService)
        {
            _guidesService = guidesService;
        }

        public IActionResult Index()
        {
            var model = _guidesService.All()?.ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
