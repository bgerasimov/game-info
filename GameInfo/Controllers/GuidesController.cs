using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInfo.Controllers
{
    public class GuidesController : Controller
    {
        private readonly IGuidesService _guidesService;
        private readonly AuthorizerService _authorizerService;
        private readonly UserManager<GameInfoUser> _userManager;
        private readonly GameInfoContext _db;

        public GuidesController(IGuidesService guidesService, GameInfoContext db, 
            AuthorizerService authorizerService, UserManager<GameInfoUser> userManager)
        {
            _guidesService = guidesService;            
            _db = db;
            _authorizerService = authorizerService;
            _userManager = userManager;
        }

        
        public IActionResult Index()
        {
            var model = _guidesService.All()?.ToList();

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
        public async Task<IActionResult> Add(AddGuideInputModel inputModel)
        {
            if (inputModel.GuideContent.Contains('<') || inputModel.GuideContent.Contains('>'))
            {
                return View();
            }

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

        public IActionResult Details(int id)
        {
            var guide = _db.Guides.FirstOrDefault(x => x.Id == id);
            guide.Creator = _db.Users.FirstOrDefault(x => x.Id == guide.CreatorId);

            if (guide == null)
            {
                return Redirect("/Guides");
            }

            var model = new GuideDetailsViewModel
            {
                Id = guide.Id,
                Title = guide.Title,
                UserName = guide.Creator.UserName,
                UserAvatar = guide.Creator.AvatarUrl
            };

            model.Content = string.Join(
                "<br/>", guide.Content.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(x => HttpUtility.HtmlEncode(x)));

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var guide = _db.Guides.FirstOrDefault(x => x.Id == id);
            if (guide != null)
            {
                _db.Guides.Remove(guide);
                await _db.SaveChangesAsync();
                return Redirect("/Guides/DeleteSuccess");
            }
            
            return Redirect("/Guides/Index");
        }
        
        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}
