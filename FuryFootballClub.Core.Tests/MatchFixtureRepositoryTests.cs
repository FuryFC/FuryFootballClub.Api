using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Repository;
using FuryFootballClub.Core.Service;
using MongoRepository;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FuryFootballClub.Core.Tests
{
    [TestFixture]
    public class MatchFixtureRepositoryTests
    {
        IMatchFixtureRepository _matchFixtureRepository;
        MatchFixture _deletable;
        MatchFixture _updateable;
        MatchFixture _find1;
        MatchFixture _find2;
        DateTime _today;
        IList<MatchFixture> _fixtures;

        [SetUp]
        public void SetUp()
        {
            _today = TimeZoneInfo.ConvertTimeToUtc(DateTime.Today);
            _matchFixtureRepository = new MatchFixtureRepository();

            /* Wipe out all match fixtures from the test database */
            var fixtures = _matchFixtureRepository.List();
            foreach(var fixture in fixtures)
            {
                _matchFixtureRepository.Delete(fixture);
            }

            /* Create some test data here */
            _deletable = new MatchFixture() { Competition = "TeamAwesome", HomeTeam = "FuryFC", AwayTeam="TeamAwesome", Field="VH Dome, field 1", MatchTime=_today.AddDays(1.0) };
            _updateable = new MatchFixture() { Competition = "TeamAwesome", HomeTeam = "TeamAwesome", AwayTeam = "FuryFC", Field = "VH Dome, field 2", MatchTime = _today.AddDays(2.0) };
            _find1 = new MatchFixture() { Competition = "BabySeals", HomeTeam = "BabySeals", AwayTeam = "FuryFC", Field = "VH Dome, field 4", MatchTime = _today.AddDays(2.0) };
            _find2 = new MatchFixture() { Competition = "BabySeals", HomeTeam = "FuryFC", AwayTeam = "BabySeals", Field = "VH Dome, field 3", MatchTime = _today.AddDays(2.0) };
            _fixtures = new List<MatchFixture>();
            _fixtures.Add(_deletable);
            _fixtures.Add(_updateable);
            _fixtures.Add(_find1);
            _fixtures.Add(_find2);
            _matchFixtureRepository.Add(_fixtures);
        }

        [TearDown]
        public void TearDown()
        {
            foreach(var fixture in _fixtures) 
            {
                _matchFixtureRepository.Delete(fixture);
            }
       }

        #region Delete

        [Test]
        public void Delete_ByGuid()
        {
            Guid guid = _deletable.Id;

            Assert.IsNotNull(_matchFixtureRepository.FindByGuid(guid));
            _matchFixtureRepository.Delete(_deletable);
            Assert.IsNull(_matchFixtureRepository.FindByGuid(guid));
        }

        #endregion

        #region Retrieve

        [Test]
        public void Retrieve_One()
        {
            var result = _matchFixtureRepository.FindByGuid(_find1.Id);

            Assert.AreEqual(_find1.Id, result.Id);
            Assert.AreEqual(_find1.Competition, result.Competition);
            Assert.AreEqual(_find1.HomeTeam, result.HomeTeam);
            Assert.AreEqual(_find1.AwayTeam, result.AwayTeam);
            Assert.AreEqual(_find1.Field, result.Field);
            Assert.AreEqual(_find1.MatchTime, result.MatchTime);
        }

        [Test]
        public void Retrieve_All()
        {
            var guid1 = _find1.Id;
            var guid2 = _find2.Id;

            bool found1 = false;
            bool found2 = false;
            var allFixtures = _matchFixtureRepository.List();
            foreach (var fixture in allFixtures)
            {
                if (guid1.Equals(fixture.Id))
                {
                    found1 = true;
                }
                if (guid2.Equals(fixture.Id))
                {
                    found2 = true;
                }
            }

            Assert.True(found1);
            Assert.True(found2);
        }

        #endregion

        #region Insert

        [Test]
        public void Insert_One()
        {
            var one = new MatchFixture() { Competition = "BabySeals2", HomeTeam = "FuryFC", AwayTeam = "BabySeals2", Field = "VH Dome, field 12", MatchTime = _today.AddDays(3.0) };
            _fixtures.Add(one);
            _matchFixtureRepository.Save(one);

            var result = _matchFixtureRepository.FindByGuid(one.Id);

            Assert.AreEqual(one.Id, result.Id);
            Assert.AreEqual(one.Competition, result.Competition);
            Assert.AreEqual(one.HomeTeam, result.HomeTeam);
            Assert.AreEqual(one.AwayTeam, result.AwayTeam);
            Assert.AreEqual(one.Field, result.Field);
            Assert.AreEqual(one.MatchTime, result.MatchTime);
        }

        [Test]
        public void Insert_Bulk()
        {
            var one = new MatchFixture() { Competition = "BabySeals3", HomeTeam = "FuryFC", AwayTeam = "BabySeals2", Field = "VH Dome, field 12", MatchTime = _today.AddDays(3.0) };
            var two = new MatchFixture() { Competition = "BabySeals4", HomeTeam = "FuryFC", AwayTeam = "BabySeals2", Field = "VH Dome, field 12", MatchTime = _today.AddDays(3.0) };
            _fixtures.Add(one);
            _fixtures.Add(two);
            _matchFixtureRepository.Add(new [] { one, two });

            var result1 = _matchFixtureRepository.FindByGuid(one.Id);
            var result2 = _matchFixtureRepository.FindByGuid(two.Id);

            Assert.AreEqual(one.Id, result1.Id);
            Assert.AreEqual(one.Competition, result1.Competition);
            Assert.AreEqual(two.Id, result2.Id);
            Assert.AreEqual(two.Competition, result2.Competition);
        }

        #endregion

        #region Update
        [Test]
        public void Update_One()
        {
            Assert.AreEqual("TeamAwesome", _updateable.Competition);
            _updateable.Competition = "TeamAwesomer";
            _matchFixtureRepository.Save(_updateable);
            Assert.AreEqual("TeamAwesomer", _updateable.Competition);
        }

        #endregion

        #region Exceptions
        // TODO: Add some exception tests
        #endregion
    }
}
