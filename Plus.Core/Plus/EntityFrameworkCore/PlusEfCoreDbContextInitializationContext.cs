using Plus.Uow;

namespace Plus.EntityFrameworkCore
{
    public class PlusEfCoreDbContextInitializationContext
    {
        public IUnitOfWork UnitOfWork { get; }

        public PlusEfCoreDbContextInitializationContext(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}