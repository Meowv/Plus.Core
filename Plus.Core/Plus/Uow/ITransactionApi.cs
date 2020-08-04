using System;
using System.Threading.Tasks;

namespace Plus.Uow
{
    public interface ITransactionApi : IDisposable
    {
        Task CommitAsync();
    }
}