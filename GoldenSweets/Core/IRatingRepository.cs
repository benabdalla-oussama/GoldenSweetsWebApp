using GoldenSweets.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core
{
    public interface IRatingRepository
    {
            
            Task<Rating> GetUserRatingByCakeAsync(int cakeId);

            Task<string> GetRatingNumberByCake(int cakeId);

            Task<Rating> GetRatingById(int ratingId);
            void UpdateRating(Rating rating);
            Task AddRatingAsync(Rating rating);
            void Delete(int id);
        
    }
}
