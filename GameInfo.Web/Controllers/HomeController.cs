using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameInfo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Data;

namespace GameInfo.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IGuidesService _guidesService;
        private readonly GameInfoContext _db;

        public HomeController(IGuidesService guidesService, GameInfoContext db)
        {
            _guidesService = guidesService;
            _db = db;
        }

        public IActionResult Index()
        {
            var model = _guidesService.All()?.ToList();

            return View(model);
        }

        public IActionResult Search(string searchQuery)
        {
            var model = GetSearchResults(searchQuery);

            return View(model);
        }

        private SearchViewModel GetSearchResults(string searchQuery)
        {
            var model = new SearchViewModel()
            {
                Guides = _db.Guides.Where(x => x.Title == searchQuery || x.Title.Contains(searchQuery)),
                Items = _db.Items.Where(x => x.Name == searchQuery || x.Name.Contains(searchQuery)),
                NPCs = _db.NPCs.Where(x => x.Name == searchQuery || x.Name.Contains(searchQuery)),
                Races = _db.Races.Where(x => x.Name == searchQuery || x.Name.Contains(searchQuery)),
                Professions = _db.Professions.Where(x => x.Name == searchQuery || x.Name.Contains(searchQuery)),
                Quests = _db.Quests.Where(x => x.Title == searchQuery || x.Title.Contains(searchQuery)),
                Dungeons = _db.Dungeons.Where(x => x.Name == searchQuery || x.Name.Contains(searchQuery)),
                Achievements = _db.Achievements.Where(x => x.Name == searchQuery || x.Name.Contains(searchQuery)),
            };

            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
