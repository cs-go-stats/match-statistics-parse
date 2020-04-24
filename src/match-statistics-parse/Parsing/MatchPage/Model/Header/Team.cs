﻿using System.Diagnostics;
using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Header
{
    [ModelLeaf]
    [DebuggerDisplay("Team = {Name}; Score = {ScoreCode}/{Score}.")]
    public class Team
    {
        [RequiredContainer("a/img"), ElementTitleValue]
        public string Name { get; private set; }

        [RequiredContainer("a"), AnchorLinkValue]
        public string Link { get; private set; }

        [RequiredContainer("div"), ScoreCodeValue]
        public string ScoreCode { get; private set; }

        [RequiredContainer("div"), IntegerValue]
        public int Score { get; private set; }
    }
}