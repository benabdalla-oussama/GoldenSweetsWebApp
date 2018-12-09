using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core
{
    public interface IUserService
    {
        Task<string> GetUserId();
    }
}
