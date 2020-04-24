using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage;
using Xunit;

namespace CSGOStats.Services.MatchStatisticsParse.Tests
{
    public class MapPageParseTests
    {
        [Fact]
        public async Task MapWithOvertimesTest()
        {
            await new MapPageParser().ParseAsync(new FileContentLoader("Pages/map-page-v1.htm"));
        }

        [Fact]
        public async Task RegularMapScoreTest()
        {
            await new MapPageParser().ParseAsync(new FileContentLoader("Pages/map-page-v2.htm"));
        }
    }
}