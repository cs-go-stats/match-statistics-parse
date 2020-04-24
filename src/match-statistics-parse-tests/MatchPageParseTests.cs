using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage;
using Xunit;

namespace CSGOStats.Services.MatchStatisticsParse.Tests
{
    public class MatchPageParseTests
    {
        [Fact]
        public async Task Bo3Match2PlayedMapsPageParseTest()
        {
            await new MatchPageParser().ParseAsync(new FileContentLoader("Pages/match-page-v1.htm"));
        }

        [Fact]
        public async Task Bo3MatchForfeitedGamePageParseTest()
        {
            await new MatchPageParser().ParseAsync(new FileContentLoader("Pages/match-page-v2.htm"));
        }
    }
}