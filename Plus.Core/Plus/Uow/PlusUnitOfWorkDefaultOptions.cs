using System.Data;

namespace Plus.Uow
{
    //TODO: Implement default options!

    /// <summary>
    /// Global (default) unit of work options
    /// </summary>
    public class PlusUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Default value: <see cref="UnitOfWorkTransactionBehavior.Auto"/>.
        /// </summary>
        public UnitOfWorkTransactionBehavior TransactionBehavior { get; set; } = UnitOfWorkTransactionBehavior.Auto;

        public IsolationLevel? IsolationLevel { get; set; }

        public int? Timeout { get; set; }

        internal PlusUnitOfWorkOptions Normalize(PlusUnitOfWorkOptions options)
        {
            if (options.IsolationLevel == null)
            {
                options.IsolationLevel = IsolationLevel;
            }

            if (options.Timeout == null)
            {
                options.Timeout = Timeout;
            }

            return options;
        }

        public bool CalculateIsTransactional(bool autoValue)
        {
            switch (TransactionBehavior)
            {
                case UnitOfWorkTransactionBehavior.Enabled:
                    return true;
                case UnitOfWorkTransactionBehavior.Disabled:
                    return false;
                case UnitOfWorkTransactionBehavior.Auto:
                    return autoValue;
                default:
                    throw new PlusException("Not implemented TransactionBehavior value: " + TransactionBehavior);
            }
        }
    }
}