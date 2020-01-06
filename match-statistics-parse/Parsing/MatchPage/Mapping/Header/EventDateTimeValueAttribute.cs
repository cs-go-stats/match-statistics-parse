using System;
using CSGOStats.Infrastructure.PageParse.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EventDateTimeValueAttribute : BaseMapValueAttribute
    {
        public EventDateTimeValueAttribute()
            : base(EventDateTimeValueMapper.EventDateTimeValueCode)
        {
        }
    }
}