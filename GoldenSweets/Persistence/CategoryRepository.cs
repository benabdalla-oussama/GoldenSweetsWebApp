using GoldenSweets.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldenSweets.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GoldenSweetsDbContext _context;

        public CategoryRepository(GoldenSweetsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
