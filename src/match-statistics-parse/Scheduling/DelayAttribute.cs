using System;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DelayAttribute : Attribute
    {
        public int Seconds { get; }

        public DelayAttribute(int seconds)
        {
            Seconds = seconds.Natural(nameof(seconds));
        }
    }
}