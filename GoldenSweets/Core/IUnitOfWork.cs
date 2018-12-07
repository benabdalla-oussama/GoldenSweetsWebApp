using System.Threading.Tasks;

namespace GoldenSweets.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
