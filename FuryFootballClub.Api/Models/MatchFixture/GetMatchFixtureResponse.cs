using System;

namespace FuryFootballClub.Api.Models.MatchFixture
{
    public class GetMatchFixtureResponse
    {
        public ResponseStatus Status { get; set; }
        public Guid Guid { get; set; }
        public MatchFixtureData MatchFixtureData { get; set; }
    }

    // TODO: move to own file
    // TODO: convert stringly typed to strongly typed
    public class MatchFixtureData
    {
        public string Competition { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Field { get; set; }
        public DateTime MatchTime { get; set; }
    }
}