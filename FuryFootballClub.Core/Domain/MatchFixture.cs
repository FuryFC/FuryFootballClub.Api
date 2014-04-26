using System;

namespace FuryFootballClub.Core.Domain
{
    // TODO: convert to strongly typed
    public class MatchFixture
    {
        public Guid Id { get; set; }
        public string Competition { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Field { get; set; }
        public DateTime? MatchTime { get; set; }
    }
}
