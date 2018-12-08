using GoldenSweets.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldenSweets.Web.Core;
using GoldenSweets.Web.Persistence;

namespace GoldenSweets.Persistence
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly GoldenSweetsDbContext _context;

        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }


        private  ShoppingCartService(GoldenSweetsDbContext context)
        {
            _context = context;
            
        }

        public async static Task<string> getCurrentUserId(UserManager<IdentityUser> userManager, HttpContext context)
        {
            var userId = userManager.GetUserId(context.User);
            //var user = await userManager.FindByNameAsync(context.User?.Identity?.Name);
            return userId;
        }

        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            var context = services.GetRequiredService<GoldenSweetsDbContext>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();


            var userId = getCurrentUserId(userManager,httpContext) ;
            var id = userId.ToString() ?? Guid.NewGuid().ToString();

            return new ShoppingCartService(context)
            {
                Id = id
            };
        }

        public async Task<int> AddToCartAsync(Cake cake, int qty = 1)
        {
            return await AddOrRemoveCart(cake, qty);

        }

        public async Task<int> RemoveFromCartAsync(Cake cake)
        {
            return await AddOrRemoveCart(cake, -1);
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            ShoppingCartItems = ShoppingCartItems ?? await _context.ShoppingCartItems
                .Where(e => e.ShoppingCartId == Id)
                .Include(e => e.Cake)
                .ToListAsync();

            return ShoppingCartItems;
        }

        public async Task ClearCartAsync()
        {
            var shoppingCartItems = _context
                .ShoppingCartItems
                .Where(s => s.ShoppingCartId == Id);

            _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

            ShoppingCartItems = null; //reset
            await _context.SaveChangesAsync();
        }

        public async Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync()
        {
            var subTotal = ShoppingCartItems?
                .Select(c => c.Cake.Price * c.Qty) ??
                await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == Id)
                .Select(c => c.Cake.Price * c.Qty)
                .ToListAsync();

            return (subTotal.Count(), subTotal.Sum());
        }

        private async Task<int> AddOrRemoveCart(Cake cake, int qty)
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                            .SingleOrDefaultAsync(s => s.CakeId == cake.Id && s.ShoppingCartId == Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Cake = cake,
                    Qty = 0
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }

            shoppingCartItem.Qty += qty;

            if (shoppingCartItem.Qty <= 0)
            {
                shoppingCartItem.Qty = 0;
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await _context.SaveChangesAsync();

            ShoppingCartItems = null; // Reset

            return await Task.FromResult(shoppingCartItem.Qty);
        }

    }
}
