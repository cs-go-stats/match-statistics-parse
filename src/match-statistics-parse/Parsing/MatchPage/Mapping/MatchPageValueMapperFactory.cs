using System;
using CSGOStats.Infrastructure.PageParse.Mapping;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Veto;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping
{
    public class MatchPageValueMapperFactory : IValueMapperFactory
    {
        public IValueMapper Create(string mapperCode)
        {
            switch (mapperCode)
            {
                case ScoreCodeValueMapper.ScoreCodeValue:
                    return new ScoreCodeValueMapper();
                case EventDateTimeValueMapper.EventDateTimeValueCode:
                    return new EventDateTimeValueMapper();
                case VetoValueMapper.VetoMapperCode:
                    return new VetoValueMapper();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperCode), "Unknown mapper");
            }
        }
    }
}