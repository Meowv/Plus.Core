using JetBrains.Annotations;
using System.Data;

namespace Plus.Uow
{
    public static class UnitOfWorkManagerExtensions
    {
        [NotNull]
        public static IUnitOfWork Begin(
            [NotNull] this IUnitOfWorkManager unitOfWorkManager,
            bool requiresNew = false,
            bool isTransactional = false,
            IsolationLevel? isolationLevel = null,
            int? timeout = null)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));

            return unitOfWorkManager.Begin(new PlusUnitOfWorkOptions
            {
                IsTransactional = isTransactional,
                IsolationLevel = isolationLevel,
                Timeout = timeout
            }, requiresNew);
        }

        public static void BeginReserved([NotNull] this IUnitOfWorkManager unitOfWorkManager, [NotNull] string reservationName)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));
            Check.NotNull(reservationName, nameof(reservationName));

            unitOfWorkManager.BeginReserved(reservationName, new PlusUnitOfWorkOptions());
        }

        public static void TryBeginReserved([NotNull] this IUnitOfWorkManager unitOfWorkManager, [NotNull] string reservationName)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));
            Check.NotNull(reservationName, nameof(reservationName));

            unitOfWorkManager.TryBeginReserved(reservationName, new PlusUnitOfWorkOptions());
        }
    }
}