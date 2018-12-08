using GoldenSweets.Web.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoldenSweets.Web.Persistence
{
    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(IHttpContextAccessor context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IdentityUser> GetUser()
        {
            return await _userManager.FindByEmailAsync(_context.HttpContext.User?.Identity?.Name);
        }

        public async Task<string> GetUserId()
        {
            var user = await _userManager.FindByEmailAsync(_context.HttpContext.User?.Identity?.Name);
            return user.Id;
        }
    }
}
