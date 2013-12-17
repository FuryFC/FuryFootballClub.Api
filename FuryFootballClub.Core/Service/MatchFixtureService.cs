using System;
using System.Collections.Generic;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Repository;

namespace FuryFootballClub.Core.Service
{
    public class MatchFixtureService : IMatchFixtureService
    {
        private readonly IMatchFixtureRepository _matchFixtureRepository;

        public MatchFixtureService(IMatchFixtureRepository matchFixtureRepository)
        {
            _matchFixtureRepository = matchFixtureRepository;
        }

        public void Delete(Guid guid)
        {
            throw new System.NotImplementedException();
        }

        public IList<MatchFixture> List()
        {
            throw new System.NotImplementedException();
        }

        public Guid Save(MatchFixture matchFixture)
        {
            throw new System.NotImplementedException();
        }

        public MatchFixture Find(Guid guid)
        {
            throw new System.NotImplementedException();
        }
    }
}
