using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Hosting;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FuryFootballClub.Api.Controllers;
using FuryFootballClub.Api.Models.MatchFixture;
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

            // Mocking Context for the controller
            var request = MockRepository.GenerateMock<HttpRequestMessage>();
            _controller.Request = request;
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region Delete

        [Test]
        public void Delete_RemovingExistingMatchFixture()
        {
            var matchFixtureGuid = Guid.NewGuid();
            _matchFixtureService.Expect(s => s.Delete(matchFixtureGuid));

            var result = _controller.Delete(matchFixtureGuid);

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        #endregion

        #region Get By Guid

        [Test]
        public void Get_MatchFixtureByGuid()
        {
            var matchFixtureDto = new GetMatchFixtureRequest {Guid=Guid.NewGuid()};
            var matchFixture = new MatchFixture() {Guid = matchFixtureDto.Guid};
            var expectedMatchFixture = new MatchFixtureData();

            _mapper.Expect(m => m.Map<GetMatchFixtureRequest, MatchFixture>(matchFixtureDto)).Return(matchFixture);
            _mapper.Expect(m => m.Map<MatchFixture, MatchFixtureData>(matchFixture)).Return(expectedMatchFixture);
            _matchFixtureService.Expect(s => s.Find(matchFixtureDto.Guid)).Return(matchFixture);

            var result = _controller.Get(matchFixtureDto);

            Assert.AreSame(expectedMatchFixture, result);
        }

        [Test]
        public void Get_GuidDoesExist()
        {
            var matchFixtureDto = new GetMatchFixtureRequest { Guid = Guid.Empty };
            var matchFixture = new MatchFixture() { Guid = matchFixtureDto.Guid };

            _mapper.Expect(m => m.Map<GetMatchFixtureRequest, MatchFixture>(matchFixtureDto)).Return(matchFixture);
            _matchFixtureService.Expect(s => s.Find(matchFixture.Guid)).Return(null);

            var result = _controller.Get(matchFixtureDto);

            Assert.IsNull(result);
        }

        #endregion

        #region Get List

        [Test]
        public void Get_All()
        {
            var matchFixtures = new List<MatchFixture>();
            matchFixtures.Add(new MatchFixture { Guid = Guid.Empty });
            matchFixtures.Add(new MatchFixture { Guid = Guid.Empty });
            matchFixtures.Add(new MatchFixture { Guid = Guid.Empty });

            _matchFixtureService.Expect(s => s.List()).Return(matchFixtures);

            Mapper.CreateMap<MatchFixture, MatchFixtureData>();
            var result = _controller.Get();

            Assert.IsTrue(result.ToList()[0].Guid == matchFixtures[0].Guid);
            Assert.IsTrue(result.ToList()[1].Guid == matchFixtures[1].Guid);
            Assert.IsTrue(result.ToList()[2].Guid == matchFixtures[2].Guid);
        }

        #endregion

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

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            // TODO: Test newly added resource URI.
            //Assert.AreEqual(matchFixtureGuid, result.Guid);
        }

        [Test]
        public void Post_FailsWithMatchFixtureDto()
        {
            var result = _controller.Post(null);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            // TODO: Test newly added resource URI.
            //Assert.AreEqual(Guid.Empty, result.Guid);
        }

        #endregion

        #region Put

        [Test]
        public void Put_UpdateExistingMatchFixture()
        {
            var matchFixtureDto = new UpdateMatchFixtureRequest {Guid = Guid.NewGuid()};
            var matchFixture = new MatchFixture();
            //var newUri = string.Format("http://localhost:8080/MatchFixture/{0}", matchFixtureDto.Guid);


            _mapper.Expect(m => m.Map<UpdateMatchFixtureRequest, MatchFixture>(matchFixtureDto)).Return(matchFixture);
            _matchFixtureService.Expect(s => s.Save(matchFixture)).Return(matchFixtureDto.Guid);

            HttpResponseMessage result = _controller.Put(matchFixtureDto);

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            // TODO: Test newly added resource URI.
            //Assert.AreEqual(newUri, result.Headers.Location);
        }

        [Test]
        public void Put_FailsWithMissingGuid()
        {
            var matchFixtureDto = new UpdateMatchFixtureRequest();
            var result = _controller.Put(matchFixtureDto);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        #endregion
    }
}
