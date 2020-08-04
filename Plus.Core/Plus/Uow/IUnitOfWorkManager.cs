using JetBrains.Annotations;

namespace Plus.Uow
{
    public interface IUnitOfWorkManager
    {
        [CanBeNull]
        IUnitOfWork Current { get; }

        [NotNull]
        IUnitOfWork Begin([NotNull] PlusUnitOfWorkOptions options, bool requiresNew = false);

        [NotNull]
        IUnitOfWork Reserve([NotNull] string reservationName, bool requiresNew = false);

        void BeginReserved([NotNull] string reservationName, [NotNull] PlusUnitOfWorkOptions options);

        bool TryBeginReserved([NotNull] string reservationName, [NotNull] PlusUnitOfWorkOptions options);
    }
}