using FuryFootballClub.Core.Repository;
using FuryFootballClub.Core.Service;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Core.Tests
{
    [TestFixture]
    public class MatchFixtureServiceTests
    {
        private IMatchFixtureRepository _matchFixtureRepository;
        private MatchFixtureService _matchFixtureService;

        [SetUp]
        public void SetUp()
        {
            _matchFixtureRepository = MockRepository.GenerateMock<IMatchFixtureRepository>();
            _matchFixtureService = new MatchFixtureService(_matchFixtureRepository);
        }

        #region Delete
        [Test]
        public void Delete_One()
        {
            var matchFixtureGuid = Guid.NewGuid();

            _matchFixtureRepository.Expect(s => s.Delete(matchFixtureGuid));

            _matchFixtureService.Delete(matchFixtureGuid);
        }

        #endregion

        #region List
        [Test]
        public void List_All()
        {
            var fixtures = new[] 
            { 
                new MatchFixture() { Id = Guid.NewGuid(), Competition="blah" },
                new MatchFixture() { Id = Guid.NewGuid(), Competition="blah2" }
            };

            _matchFixtureRepository.Expect(s => s.List()).Return(fixtures);

            var result = _matchFixtureService.List();

            Assert.AreSame(fixtures,result);
        }
        #endregion

        #region Save
        [Test]
        public void Save_One()
        {
            var fixture = new MatchFixture() { Id = Guid.NewGuid(), Competition = "blah3" };

            _matchFixtureRepository.Expect(s => s.Save(fixture));

            var guid = _matchFixtureService.Save(fixture);

            Assert.AreEqual(fixture.Id, guid);
        }
        #endregion
        
        #region Find
        [Test]
        public void Find_One()
        {
            var matchFixtureGuid = Guid.NewGuid();

            _matchFixtureRepository.Expect(s => s.FindByGuid(matchFixtureGuid));

            _matchFixtureService.Find(matchFixtureGuid);
        }
        #endregion
    }
}
