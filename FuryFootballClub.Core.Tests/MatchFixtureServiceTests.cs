using FuryFootballClub.Core.Repository;
using FuryFootballClub.Core.Service;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace FuryFootballClub.Core.Tests
{
    [TestFixture]
    public class MatchFixtureServiceTests
    {
        private IMatchFixtureRepository _matchFixtureRepository;
        private MatchFixtureService _matchFixtureService;
        private static readonly ILog log = LogManager.GetLogger(typeof(MatchFixtureServiceTests));

        [SetUp]
        public void SetUp()
        {
            _matchFixtureRepository = MockRepository.GenerateMock<IMatchFixtureRepository>();
            _matchFixtureService = new MatchFixtureService(_matchFixtureRepository);
        }
    }
}
