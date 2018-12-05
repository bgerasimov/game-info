using GameInfo.Models.Enums;
using GameInfo.Models.InputModels;
using GameInfo.Services.Authorization;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Controllers
{
    public class ProfessionsController : Controller
    {
        private readonly IProfessionsService _professionsService;
        private readonly AuthorizerService _authorizerService;

        public ProfessionsController(IProfessionsService professionsService, AuthorizerService authorizerService)
        {
            _professionsService = professionsService;
            _authorizerService = authorizerService;
        }

        public IActionResult Index()
        {
            var model = _professionsService.All()?.ToList();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var user = await _authorizerService.Authorize(HttpContext);

            if (user != null)
            {
                var model = new AddProfessionInputModel
                {
                    ClassRoles = Enum.GetNames(typeof(ClassRole)).ToList(),
                    CombatTypes = Enum.GetNames(typeof(CombatType)).ToList(),
                    WeaponTypes = Enum.GetNames(typeof(WeaponType)).ToList()
                };

                return View(model);
            }

            return Redirect("/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddProfessionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new AddProfessionInputModel
                {
                    ClassRoles = Enum.GetNames(typeof(ClassRole)).ToList(),
                    CombatTypes = Enum.GetNames(typeof(CombatType)).ToList(),
                    WeaponTypes = Enum.GetNames(typeof(WeaponType)).ToList()
                };

                return View(model);
            }

            _professionsService.Add(inputModel);

            return Redirect("/Professions");
        }
    }
}
