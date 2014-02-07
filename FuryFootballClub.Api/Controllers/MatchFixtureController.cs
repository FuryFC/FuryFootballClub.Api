using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FuryFootballClub.Api.Models.MatchFixture;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Service;

namespace FuryFootballClub.Api.Controllers
{
    public class MatchFixtureController : ApiController
    {
        private readonly IMatchFixtureService _matchFixtureService;
        private readonly IMappingEngine _mapper;

        // TODO: Add Dependency injection
        public MatchFixtureController()
        {
            _matchFixtureService = new MatchFixtureService();
            var config = new AutoMapperConfig();
            config.RegisterModelMappings();
            _mapper = Mapper.Engine;
        }

        public MatchFixtureController(IMatchFixtureService matchFixtureService, IMappingEngine mapper)
        {
            _matchFixtureService = matchFixtureService;
            _mapper = mapper;
        }

        public HttpResponseMessage Delete(Guid guid)
        {
            _matchFixtureService.Delete(guid);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [ActionName("GetAll")]
        public IQueryable<MatchFixtureData> GetAll()
        {
            var matchFixtures = _matchFixtureService.List();
            return matchFixtures.AsQueryable().Project().To<MatchFixtureData>();
        }

        [ActionName("Get")]
        public MatchFixtureData Get(Guid id)
        {
            if (id == null) { throw new HttpRequestException("Missing the ID for a single get request"); }

            var matchFixture = _matchFixtureService.Find(id);
            var matchFixtureData = matchFixture == null ? null : _mapper.Map<MatchFixture, MatchFixtureData>(matchFixture);
            return matchFixtureData;
        }

        public HttpResponseMessage Post(NewMatchFixtureRequest newMatchFixtureRequest)
        {
            // TODO: Send back validation errors
            if (newMatchFixtureRequest == null) { return Request.CreateResponse(HttpStatusCode.BadRequest); }

            var matchFixture = _mapper.Map<NewMatchFixtureRequest, MatchFixture>(newMatchFixtureRequest);
            _matchFixtureService.Save(matchFixture);

            return Request.CreateResponse(HttpStatusCode.OK, matchFixture);
        }

        public HttpResponseMessage Put(UpdateMatchFixtureRequest updateMatchFixtureRequest)
        {
            // TODO: Send back validation errors
            if (updateMatchFixtureRequest.Id == Guid.Empty) { return Request.CreateResponse(HttpStatusCode.BadRequest); }

            var matchFixture = _mapper.Map<UpdateMatchFixtureRequest, MatchFixture>(updateMatchFixtureRequest);
            _matchFixtureService.Save(matchFixture);

            return Request.CreateResponse(HttpStatusCode.NoContent, matchFixture);
        }
    }
}
