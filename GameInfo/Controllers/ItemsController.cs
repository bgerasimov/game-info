﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;
        private readonly AuthorizerService _authorizerService;
        private readonly GameInfoContext _db;

        public ItemsController(AuthorizerService authorizerService, GameInfoContext db, IItemsService itemsService)
        {
            _authorizerService = authorizerService;
            _db = db;
            _itemsService = itemsService;
        }

        public IActionResult Index()
        {
            var model = _itemsService.All()?.ToList();

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
        public IActionResult Add(AddItemInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var item = new Item
            {
                Name = inputModel.Name,
                AcquiredFrom = inputModel.AcquiredFrom,
                Usage = inputModel.Usage
            };
        
            this._db.Items.Add(item);
            this._db.SaveChanges();
        
            return Redirect("/Items");
        }

        public IActionResult Details(int id)
        {
            var item = _db.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return Redirect("/Items");
            }

            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = _db.Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _db.Items.Remove(item);
                await _db.SaveChangesAsync();
                return Redirect("/Items/DeleteSuccess");
            }

            return Redirect("/Items/Index");
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}