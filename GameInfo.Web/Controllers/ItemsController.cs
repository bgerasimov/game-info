using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using GameInfo.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class ItemsController : Controller
    {
        private const string Items_Root_Path = "/Items";
        private readonly IItemsService _itemsService;
        private readonly AuthorizerService _authorizerService;

        public ItemsController(AuthorizerService authorizerService, IItemsService itemsService)
        {
            _authorizerService = authorizerService;
            _itemsService = itemsService;
        }

        public IActionResult Index()
        {
            var model = _itemsService.All()?.ToList();

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
        public IActionResult Add(AddItemInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _itemsService.Add(inputModel);
        
            return Redirect(Items_Root_Path);
        }

        public IActionResult Details(int id)
        {
            var item = _itemsService.ById(id);

            if (item == null)
            {
                return Redirect(Items_Root_Path);
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var success = _itemsService.Delete(id);

            if (success)
            {
                return Redirect("/Items/DeleteSuccess");
            }

            return Redirect(Items_Root_Path);
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}