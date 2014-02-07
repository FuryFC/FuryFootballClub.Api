using System;
using System.Collections.Generic;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Repository;

namespace FuryFootballClub.Core.Service
{
    public class MatchFixtureService : IMatchFixtureService
    {
        private readonly IMatchFixtureRepository _matchFixtureRepository;

        // TODO: Use dependency injection here instead of this default constructor
        public MatchFixtureService()
        {
            _matchFixtureRepository = new MatchFixtureRepository();
        }

        public MatchFixtureService(IMatchFixtureRepository matchFixtureRepository)
        {
            _matchFixtureRepository = matchFixtureRepository;
        }

        public void Delete(Guid guid)
        {
            _matchFixtureRepository.Delete(guid);
        }

        public IEnumerable<MatchFixture> List()
        {
            return _matchFixtureRepository.List();
        }

        public Guid Save(MatchFixture matchFixture)
        {
            _matchFixtureRepository.Save(matchFixture);
            return matchFixture.Id;
        }

        public MatchFixture Find(Guid guid)
        {
            return _matchFixtureRepository.FindByGuid(guid);
        }
    }
}
