using System;
using System.Linq;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Core.Service
{
    public interface IMatchFixtureService
    {
        void Delete(Guid guid);
        IQueryable<MatchFixture> List();
        Guid Save(MatchFixture matchFixture);
        MatchFixture Find(Guid guid);
    }
}
