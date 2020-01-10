using CSGOStats.Infrastructure.DataAccess.Contexts.Mongo;
using CSGOStats.Services.Core.Handling.Entities;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using MongoDB.Bson.Serialization;

namespace CSGOStats.Services.MatchStatisticsParse.Data.Mongo
{
    public static class DocumentMapping
    {
        public static void Registration()
        {
            BsonClassMap.RegisterClassMap<AggregateRoot>(mapper =>
            {
                mapper.MapGuid(x => x.Id);
                mapper.MapRequired(x => x.Version);
            });

            BsonClassMap.RegisterClassMap<Game>(mapper =>
            {
                mapper.MapOptional(x => x.Link);
                mapper.MapOffsetDateTime(x => x.DateTime);
                mapper.MapOptional(x => x.Event);
                mapper.MapOptional(x => x.Rosters);
                mapper.MapOptional(x => x.Maps);
                mapper.MapOptional(x => x.Statistics);
                mapper.MapOptional(x => x.Score);
                mapper.MapCreator(x => new Game(x.Id, x.Version, x.Link, x.DateTime, x.Event, x.Rosters, x.Maps, x.Statistics, x.Score));
                mapper.MapCreator(x => new Game(x.Id, x.Version));
                mapper.MapCreator(x => new Game(x.Id, x.Version, x.Link));
                mapper.MapCreator(x => new Game(x.Id, x.Version, x.Link, x.DateTime, x.Event, x.Maps));
            });

            BsonClassMap.RegisterClassMap<Event>(mapper =>
            {
                mapper.MapRequired(x => x.Name);
                mapper.MapRequired(x => x.Link);
                mapper.MapCreator(x => new Event(x.Name, x.Link));
            });

            BsonClassMap.RegisterClassMap<Roster>(mapper =>
            {
                mapper.MapRequired(x => x.Tag);
                mapper.MapRequired(x => x.Players);
                mapper.MapCreator(x => new Roster(x.Tag, x.Players));
            });

            BsonClassMap.RegisterClassMap<Player>(mapper =>
            {
                mapper.MapRequired(x => x.Nickname);
                mapper.MapRequired(x => x.Link);
                mapper.MapCreator(x => new Player(x.Nickname,x.Link));
            });

            BsonClassMap.RegisterClassMap<VetoedMap>(mapper =>
            {
                mapper.MapRequired(x => x.Name);
                mapper.MapRequired(x => x.Veto);
                mapper.MapCreator(x => new VetoedMap(x.Name, x.Veto));
            });

            BsonClassMap.RegisterClassMap<VetoStep>(mapper =>
            {
                mapper.MapRequired(x => x.Order);
                mapper.MapOptional(x => x.Team);
                mapper.MapRequired(x => x.Decision);
                mapper.MapCreator(x => new VetoStep(x.Order, x.Team, x.Decision));
                mapper.MapCreator(x => new VetoStep(x.Order, x.Decision));
            });

            BsonClassMap.RegisterClassMap<Performance>(mapper =>
            {
                mapper.MapRequired(x => x.Team);
                mapper.MapRequired(x => x.Player);
                mapper.MapRequired(x => x.Statistics);
                mapper.MapRequired(x => x.Value);
                mapper.MapCreator(x => new Performance(x.Team, x.Player, x.Statistics, x.Value));
            });

            BsonClassMap.RegisterClassMap<Statistics>(mapper =>
            {
                mapper.MapRequired(x => x.Title);
                mapper.MapOptional(x => x.Description);
                mapper.MapCreator(x => new Statistics(x.Title, x.Description));
                mapper.MapCreator(x => new Statistics(x.Title));
            });

            BsonClassMap.RegisterClassMap<Score>(mapper =>
            {
                mapper.MapRequired(x => x.Team1);
                mapper.MapRequired(x => x.Team2);
                mapper.MapCreator(x => new Score(x.Team1, x.Team2));
            });

            BsonClassMap.RegisterClassMap<TeamResult>(mapper =>
            {
                mapper.MapRequired(x => x.Name);
                mapper.MapRequired(x => x.Total);
                mapper.MapRequired(x => x.Half1);
                mapper.MapRequired(x => x.Half2);
                mapper.MapRequired(x => x.Overtime);
                mapper.MapCreator(x => new TeamResult(x.Name, x.Total, x.Half1, x.Half2, x.Overtime));
            });
        }
    }
}