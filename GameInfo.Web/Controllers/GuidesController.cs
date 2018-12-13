using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Web.Common;
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
        private const string Guides_Root_Path = "/Guides";
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

        public async Task<IActionResult> Add()
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user != null)
            {
                return View();
            }

            return Redirect(GlobalConstants.Default_Login_Page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddGuideInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (inputModel.GuideContent.Contains('<') 
                || inputModel.GuideContent.Contains('>'))
            {
                return View();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            _guidesService.Add(inputModel, currentUser);

            return Redirect(Guides_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var guide = _guidesService.ById(id);

            if (guide == null)
            {
                return Redirect(Guides_Root_Path);
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _guidesService.Delete(id);

            if (success)
            {
                return Redirect("/Guides/DeleteSuccess");
            }
            
            return Redirect(Guides_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}
