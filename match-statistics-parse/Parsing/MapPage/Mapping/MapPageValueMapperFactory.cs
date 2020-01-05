using System;
using CSGOStats.Infrastructure.PageParse.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping
{
    internal class MapPageValueMapperFactory : IValueMapperFactory
    {
        public IValueMapper Create(string mapperCode)
        {
            switch (mapperCode)
            {
                case BreakdownValueMapper.BreakdownValueCode:
                    return new BreakdownValueMapper();
                case StatisticsCodeMapper.Code:
                    return new StatisticsCodeMapper();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperCode), "Unknown mapper");
            }
        }
    }
}