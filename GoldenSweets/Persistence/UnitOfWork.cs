using GoldenSweets.Core;
using System.Threading.Tasks;

namespace GoldenSweets.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoldenSweetsDbContext _context;

        public UnitOfWork(GoldenSweetsDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync() =>
            await _context.SaveChangesAsync();
    }
}
