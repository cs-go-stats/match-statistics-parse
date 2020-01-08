using System;
using System.Runtime.Serialization;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Veto
{
    [Serializable]
    public class CantDefineMapVeto : Exception
    {
        public CantDefineMapVeto(string entry)
            : base($"Can't define in entry '{entry}' map veto step.")
        {
        }

        protected CantDefineMapVeto(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}