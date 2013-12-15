using System;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Core.Service
{
    public interface IMatchFixtureService
    {
        Guid Save(MatchFixture matchFixture);
    }
}
