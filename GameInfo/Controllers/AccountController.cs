using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    public class AccountController : Controller
    {
        private const string LoginErrorMessage = "Username or password not found.";
        private readonly UserManager<GameInfoUser> _userManager;
        private readonly SignInManager<GameInfoUser> _signInManager;

        public AccountController(UserManager<GameInfoUser> userManager, SignInManager<GameInfoUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInputModel);
            }

            var user = await _userManager.FindByNameAsync(loginInputModel.Username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginInputModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", LoginErrorMessage);
            return View(loginInputModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel registerInputModel)
        {
            if (ModelState.IsValid)
            {
                if (registerInputModel.Password == registerInputModel.ConfirmPassword)
                {
                    var user = new GameInfoUser()
                    {
                        UserName = registerInputModel.Username,
                        Email = registerInputModel.Email
                    };
                    var result = await _userManager.CreateAsync(user, registerInputModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }                
            }            
            return View(registerInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}