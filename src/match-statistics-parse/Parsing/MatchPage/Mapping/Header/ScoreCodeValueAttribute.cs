using System;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ScoreCodeValueAttribute : BaseMapValueAttribute
    {
        public ScoreCodeValueAttribute()
            : base(ScoreCodeValueMapper.ScoreCodeValue)
        {
        }
    }
}