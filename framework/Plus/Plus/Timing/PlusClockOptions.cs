using System;

namespace Plus.Timing
{
    public class PlusClockOptions
    {
        /// <summary>
        /// Default: <see cref="DateTimeKind.Unspecified"/>
        /// </summary>
        public DateTimeKind Kind { get; set; }

        public PlusClockOptions()
        {
            Kind = DateTimeKind.Unspecified;
        }
    }
}