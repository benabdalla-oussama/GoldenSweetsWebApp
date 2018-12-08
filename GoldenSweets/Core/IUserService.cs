using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Web.Core
{
    public interface IUserService
    {
        Task<IdentityUser> GetUser();
        Task<string> GetUserId();
    }
}
