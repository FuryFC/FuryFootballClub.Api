using System;
using System.Collections.Generic;
using System.Linq;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Core.Service
{
    public interface IMatchFixtureService
    {
        void Delete(Guid guid);
        IEnumerable<MatchFixture> List();
        Guid Save(MatchFixture matchFixture);
        MatchFixture Find(Guid guid);
    }
}
