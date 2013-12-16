using System;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Core.Service
{
    public interface IMatchFixtureService
    {
        void Delete(Guid guid);
        Guid Save(MatchFixture matchFixture);
        MatchFixture Find(Guid guid);
    }
}
