using System.Threading;
using System.Threading.Tasks;

namespace Plus.Uow
{
    public interface ISupportsRollback
    {
        void Rollback();

        Task RollbackAsync(CancellationToken cancellationToken);
    }
}