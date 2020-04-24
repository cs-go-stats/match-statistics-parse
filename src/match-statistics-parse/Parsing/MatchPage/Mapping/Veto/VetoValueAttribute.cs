using System;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Veto
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VetoValueAttribute : BaseMapValueAttribute
    {
        public VetoValueAttribute()
            : base(VetoValueMapper.VetoMapperCode)
        {
        }
    }
}