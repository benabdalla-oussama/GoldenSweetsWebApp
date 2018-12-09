using GoldenSweets.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoldenSweets.Persistence
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
        
        public async Task<string> GetUserId()
        {
            var userId = _userManager.GetUserId(_context.HttpContext.User);
            return userId;
        }
    }
}
