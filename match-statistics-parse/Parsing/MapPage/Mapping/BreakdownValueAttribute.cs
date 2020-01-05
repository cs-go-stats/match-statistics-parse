using System;
using CSGOStats.Infrastructure.PageParse.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class BreakdownValueAttribute : BaseMapValueAttribute
    {
        public BreakdownValueAttribute()
            : base(BreakdownValueMapper.BreakdownValueCode)
        {
        }
    }
}