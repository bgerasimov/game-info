using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<GameInfoUser> _userManager;
        private readonly GameInfoContext _db;

        public GuidesController(IGuidesService guidesService, UserManager<GameInfoUser> userManager, GameInfoContext db)
        {
            _guidesService = guidesService;
            _userManager = userManager;
            _db = db;
        }

        
        public IActionResult Index()
        {
            var model = _guidesService.All()?.ToList();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return View();
            }

            return Redirect("/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddGuideInputModel inputModel)
        {
            var guide = new Guide
            {
                Title = inputModel.GuideTitle,
                Content = inputModel.GuideContent                
            };            
            var currentUser = await _userManager.GetUserAsync(User);
            guide.Creator = currentUser;

            this._db.Guides.Add(guide);
            this._db.SaveChanges();

            return Redirect("/Guides");
        }
    }
}
