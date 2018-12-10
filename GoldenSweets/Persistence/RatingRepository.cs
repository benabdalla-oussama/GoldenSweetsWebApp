using GoldenSweets.Core;
using GoldenSweets.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Persistence
{
    public class RatingRepository : IRatingRepository
    {
        private readonly GoldenSweetsDbContext _context;
        private readonly IUserService _userService;


        public RatingRepository(GoldenSweetsDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Rating> GetRatingById(int ratingId)
        {
            return await _context.Ratings.SingleOrDefaultAsync(e => e.Id == ratingId);
        }

        public async Task<IEnumerable<Rating>> GetRatingsByCake(int cakeId)
        {
            return await _context.Ratings
                .Where(e => e.CakeId == cakeId)
                .Include(e => e.Cake)
                .ToListAsync();
        }

        public async Task<double> GetRatingNumberByCake(int cakeId)
        {
            IEnumerable<Rating> ratings = await GetRatingsByCake(cakeId);
            int som = 0;
            foreach (var rating in ratings)
            {
                som += rating.Value;
            }
            if (ratings.Count() == 0) return 0;
            return som / ratings.Count();
        }

        public async Task<Rating> GetUserRatingByCakeAsync(int cakeId)
        {
            string userId = await _userService.GetUserId();
            return await _context.Ratings.SingleOrDefaultAsync(e => (e.CakeId == cakeId) && (e.UserId == userId));
        }

        public async void UpdateRating(Rating rating)
        {
            _context.Update(rating);
            await _context.SaveChangesAsync();
        }
        public async Task AddRatingAsync(Rating rating)
        {
             _context.Add(rating);
             await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var rating = new Rating { Id = id };
            _context.Entry(rating).State = EntityState.Deleted;
        }

    }
}
