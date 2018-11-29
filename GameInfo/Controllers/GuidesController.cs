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

        public GuidesController(IGuidesService guidesService, AuthorizerService authorizerService, UserManager<GameInfoUser> userManager)
        {
            _guidesService = guidesService;
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
            if (inputModel.GuideContent.Contains('<') 
                || inputModel.GuideContent.Contains('>')
                || !ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            _guidesService.Add(inputModel, currentUser);

            return Redirect("/Guides");
        }

        public IActionResult Details(int id)
        {
            var guide = _guidesService.ById(id);

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

            FormatContent(guide, model);

            return View(model);
        }

        private static void FormatContent(Guide guide, GuideDetailsViewModel model)
        {
            model.Content = string.Join(
                "<br/>", guide.Content.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(x => HttpUtility.HtmlEncode(x)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _guidesService.Delete(id);

            if (success)
            {
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
