using System;
using System.Collections.Generic;
using System.Linq;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using CSGOStats.Services.HistoryParse.Objects;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats.Teams;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model;
using NodaTime;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Game : AggregateRoot
    {
        public string Link { get; private set; }

        public OffsetDateTime? DateTime { get; private set; }

        public Event Event { get; private set; }

        public IReadOnlyCollection<Roster> Rosters { get; private set; }

        public IReadOnlyCollection<VetoedMap> Maps { get; private set; }

        public IReadOnlyCollection<Performance> Statistics { get; private set; }

        [Obsolete("Score property reflects only last parsed map score which is useless in scope of whole match.")]
        public Score Score { get; private set; }

        public IReadOnlyCollection<MapScore> MapScores { get; private set; }

        public Game(Guid id, long version)
            : this(id: id, version: version, link: null)
        {
        }

        public Game(Guid id, long version, string link)
            : this(id: id, version: version, link: link, dateTime: null, @event: null, maps: null)
        {
        }

        public Game(Guid id, long version, string link, OffsetDateTime? dateTime, Event @event, IReadOnlyCollection<VetoedMap> maps)
            : this(id: id, version: version, link: link, dateTime: dateTime, @event: @event, rosters: null, maps: maps, statistics: null, mapScores: null)
        {
        }

        public Game(
            Guid id,
            long version,
            string link,
            OffsetDateTime? dateTime,
            Event @event,
            IReadOnlyCollection<Roster> rosters,
            IReadOnlyCollection<VetoedMap> maps,
            IReadOnlyCollection<Performance> statistics,
            IReadOnlyCollection<MapScore> mapScores)
            : this(id, version, link, dateTime, @event, rosters, maps, statistics, null, mapScores)
        {
        }

        public Game(
            Guid id,
            long version,
            string link,
            OffsetDateTime? dateTime,
            Event @event,
            IReadOnlyCollection<Roster> rosters,
            IReadOnlyCollection<VetoedMap> maps,
            IReadOnlyCollection<Performance> statistics,
            Score score,
            IReadOnlyCollection<MapScore> mapScores)
            : base(id, version)
        {
            Link = link;
            DateTime = dateTime;
            Event = @event;
            Rosters = rosters;
            Maps = maps;
            Statistics = statistics;
            Score = score;
            MapScores = mapScores;
        }

        internal void OnHistoricalMatchParsedUpdate(HistoricalMatchParsed @event)
        {
            Link = @event.Link.NotNull(nameof(@event.Link));

            Update();
        }

        internal void OnMatchPageParsedUpdate(MatchPage matchPage)
        {
            DateTime = matchPage.Header.MatchTime;
            Event = new Event(matchPage.Header.Event.Name, matchPage.Header.Event.Link.HltvUri());
            Maps = matchPage.Veto.Select(x => new VetoedMap(
                name: x.MapCode,
                veto: new VetoStep(
                    x.Order,
                    x.TeamCode,
                    x.Decision))).ToArrayFast();

            Update();
        }

        internal void OnMapPageParsedUpdate(MapPage mapStatistics, string map)
        {
            ActualizeRosters(mapStatistics);
            AppendStatistics(mapStatistics, map);
            UpdateScore(mapStatistics, map);

            Update();
        }

        private void ActualizeRosters(MapPage mapStatistics)
        {
            if (Rosters != null)
            {
                return;
            }

            Rosters = new[]
            {
                CreateRoster(
                    tag: mapStatistics.Teams.Team1,
                    players: mapStatistics.Statistics.Team1Players),
                CreateRoster(
                    tag: mapStatistics.Teams.Team2,
                    players: mapStatistics.Statistics.Team2Players),
            };
        }

        private void AppendStatistics(MapPage mapStatistics, string map)
        {
            var statisticsAppend = mapStatistics.Statistics.Team1Players
                .Select(x => new
                {
                    Team = mapStatistics.Teams.Team1,
                    Player = x.Player.Nickname,
                    x.Statistics
                })
                .Concat(mapStatistics.Statistics.Team2Players
                    .Select(x => new
                    {
                        Team = mapStatistics.Teams.Team2,
                        Player = x.Player.Nickname,
                        x.Statistics
                    }))
                .SelectMany(x => x.Statistics.Select(s => new
                {
                    x.Team,
                    x.Player,
                    Statistics = s
                }))
                .Select(x => new Performance(
                    team: x.Team,
                    player: x.Player,
                    statistics: FindStatByCode(x.Statistics.Code, mapStatistics.Statistics.Metrics),
                    value: x.Statistics.Value,
                    map: map));

            Statistics = Statistics == null
                ? statisticsAppend.ToArrayFast()
                : Statistics.Union(statisticsAppend).ToArrayFast();
        }

        private void UpdateScore(MapPage mapStatistics, string map)
        {
            var newScore = new MapScore(
                map: map,
                score: new Score(
                    team1: new TeamResult(
                        name: mapStatistics.Teams.Team1,
                        total: mapStatistics.Breakdown.Overall.Team1,
                        half1: mapStatistics.Breakdown.Half1.Team1,
                        half2: mapStatistics.Breakdown.Half2.Team1,
                        overtime: mapStatistics.Breakdown.Overtime?.Team1 ?? 0),
                    team2: new TeamResult(
                        name: mapStatistics.Teams.Team2,
                        total: mapStatistics.Breakdown.Overall.Team2,
                        half1: mapStatistics.Breakdown.Half1.Team2,
                        half2: mapStatistics.Breakdown.Half2.Team2,
                        overtime: mapStatistics.Breakdown.Overtime?.Team2 ?? 0)));

            MapScores = MapScores == null 
                ? new [] { newScore } 
                : MapScores.Union(newScore.ToCollection()).ToArrayFast();
        }

        private static Roster CreateRoster(string tag, IEnumerable<PlayerStatistics> players) =>
            new Roster(
                tag: tag,
                players: players
                    .Select(x => new Player(x.Player.Nickname, x.Player.Link.HltvUri()))
                    .ToArrayFast());

        private static Statistics FindStatByCode(string code, IEnumerable<StatisticsMetric> metrics)
        {
            var metric = metrics.Single(x => x.Code == code);
            return new Statistics(
                metric.Title,
                metric.Description);
        }
    }
}