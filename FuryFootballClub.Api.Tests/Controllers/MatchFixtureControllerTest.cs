using System;
using System.Text.RegularExpressions;
using AutoMapper;
using FuryFootballClub.Api.Models;
using FuryFootballClub.Api.Controllers;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Service;
using NUnit.Framework;
using Rhino.Mocks;

namespace FuryFootballClub.Api.Tests.Controllers
{
    [TestFixture]
    public class MatchFixtureControllerTest
    {
        private IMatchFixtureService _matchFixtureService;
        private IMappingEngine _mapper;
        private MatchFixtureController _controller;

        [SetUp]
        public void SetUp()
        {
            _matchFixtureService = MockRepository.GenerateMock<IMatchFixtureService>();
            _mapper = MockRepository.GenerateMock<IMappingEngine>();
            _controller = new MatchFixtureController(_matchFixtureService, _mapper);
        }

        #region Post

        [Test]
        public void Post_AddNewFixture()
        {
            var matchFixtureGuid = Guid.NewGuid();
            var matchFixtureDto = new NewMatchFixtureRequest();
            var matchFixture = new MatchFixture();

            _mapper.Expect(m => m.Map<NewMatchFixtureRequest, MatchFixture>(matchFixtureDto)).Return(matchFixture);
            _matchFixtureService.Expect(s => s.Save(matchFixture)).Return(matchFixtureGuid);
            
            var result = _controller.Post(matchFixtureDto);

            Assert.AreEqual(ResponseStatus.Success, result.Status);
            Assert.AreEqual(matchFixtureGuid, result.Guid);
        }

        [Test]
        public void Post_FailsWithMatchFixtureDto()
        {
            var result = _controller.Post(null);

            Assert.AreEqual(ResponseStatus.Failure, result.Status);
            Assert.AreEqual(Guid.Empty, result.Guid);
        }

        #endregion

        #region Put

        [Test]
        public void Put_UpdateExistingMatchFixture()
        {
            var matchFixtureDto = new UpdateMatchFixtureRequest {Guid = Guid.NewGuid()};
            var matchFixture = new MatchFixture();

            _mapper.Expect(m => m.Map<UpdateMatchFixtureRequest, MatchFixture>(matchFixtureDto)).Return(matchFixture);
            _matchFixtureService.Expect(s => s.Save(matchFixture)).Return(matchFixtureDto.Guid);

            var result = _controller.Put(matchFixtureDto);

            Assert.AreEqual(ResponseStatus.Success, result.Status);
            Assert.AreEqual(matchFixtureDto.Guid, result.Guid);
        }

        [Test]
        public void Put_FailsWithMissingGuid()
        {
            var matchFixtureDto = new UpdateMatchFixtureRequest();

            var result = _controller.Put(matchFixtureDto);

            Assert.AreEqual(ResponseStatus.Failure, result.Status);
            Assert.AreEqual(Guid.Empty, result.Guid);
        }

        #endregion
    }
}
