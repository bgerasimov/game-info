using GameInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services.Authorization
{
    public class AuthorizerService
    {
        private readonly UserManager<GameInfoUser> _userManager;

        public AuthorizerService(UserManager<GameInfoUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GameInfoUser> Authorize(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);

            return user;
        }
    }
}
