using System;

namespace FuryFootballClub.Api.Models.MatchFixture
{
    public class GetMatchFixtureRequest
    {
        public Guid Id { get; set; }
        public string Competition { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Field { get; set; }
        public DateTime MatchTime { get; set; }
    }
}