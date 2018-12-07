using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldenSweets.Core.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
