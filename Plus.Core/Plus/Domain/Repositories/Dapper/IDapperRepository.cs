using System.Data;

namespace Plus.Domain.Repositories.Dapper
{
    public interface IDapperRepository
    {
        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }
    }
}