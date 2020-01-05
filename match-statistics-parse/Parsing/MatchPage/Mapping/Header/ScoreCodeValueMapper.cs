using CSGOStats.Infrastructure.PageParse.Mapping.Specific;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header
{
    public class ScoreCodeValueMapper : ElementAttributeMapper
    {
        internal const string ScoreCodeValue = nameof(ScoreCodeValueMapper);

        public ScoreCodeValueMapper()
            : base("class")
        {
        }
    }
}