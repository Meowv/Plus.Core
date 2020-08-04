using System.Threading;
using System.Threading.Tasks;

namespace Plus.Uow
{
    public interface ISupportsSavingChanges
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}