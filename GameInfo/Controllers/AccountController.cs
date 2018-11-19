using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
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

        public async Task<IActionResult> MyAccount()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var accountViewModel = new AccountViewModel
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user),
                    AvatarUrl = user.AvatarUrl
                };

                return View(accountViewModel);
            }

            return Redirect("/Account/Login");
        }

        [HttpPost]
        public async Task<IActionResult> MyAccount(AccountViewModel accountModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                if (accountModel.AvatarUrl != null)
                {
                    user.AvatarUrl = accountModel.AvatarUrl;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        accountModel.Username = user.UserName;
                        accountModel.Email = user.Email;
                        accountModel.Roles = await _userManager.GetRolesAsync(user);
                        return View(accountModel);
                    }
                }
                return View();
            }

            return Redirect("/Account/Login");
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
                        IdentityResult resultRole;
                        if (_userManager.Users.Count() == 1)
                        {
                            resultRole = await _userManager.AddToRolesAsync(user, new string [] { "Admin", "User" });
                        }
                        else
                        {
                            resultRole = await _userManager.AddToRoleAsync(user, "User");
                        }
                        if (resultRole.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }                        
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