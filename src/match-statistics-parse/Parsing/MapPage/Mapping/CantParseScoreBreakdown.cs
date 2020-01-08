using System;
using System.Runtime.Serialization;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping
{
    [Serializable]
    public class CantParseScoreBreakdown : Exception
    {
        public string RawString { get; }

        public CantParseScoreBreakdown(string rawString)
            :base($"Can't parse breakdown expression from text '{rawString}'.")
        {
            RawString = rawString;
        }

        protected CantParseScoreBreakdown(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}