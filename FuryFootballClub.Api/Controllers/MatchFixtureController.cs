using System;
using System.Web.Http;
using AutoMapper;
using FuryFootballClub.Api.Models;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Service;
using Microsoft.Ajax.Utilities;

namespace FuryFootballClub.Api.Controllers
{
    public class MatchFixtureController : ApiController
    {
        private readonly IMatchFixtureService _matchFixtureService;
        private readonly IMappingEngine _mapper;

        public MatchFixtureController(IMatchFixtureService matchFixtureService, IMappingEngine mapper)
        {
            _matchFixtureService = matchFixtureService;
            _mapper = mapper;
        }

        public MatchFixtureResponse Post([FromBody]NewMatchFixtureRequest newMatchFixtureRequest)
        {
            if (newMatchFixtureRequest == null) { return new MatchFixtureResponse() {Status = ResponseStatus.Failure}; }

            var matchFixture = _mapper.Map<NewMatchFixtureRequest, MatchFixture>(newMatchFixtureRequest);
            var guid = _matchFixtureService.Save(matchFixture);

            return new MatchFixtureResponse {Guid = guid, Status = ResponseStatus.Success};
        }

        public MatchFixtureResponse Put([FromBody] UpdateMatchFixtureRequest updateMatchFixtureRequest)
        {
            if (updateMatchFixtureRequest.Guid == Guid.Empty) { return new MatchFixtureResponse() {Status = ResponseStatus.Failure}; }

            var matchFixture = _mapper.Map<UpdateMatchFixtureRequest, MatchFixture>(updateMatchFixtureRequest);
            var guid = _matchFixtureService.Save(matchFixture);

            return new MatchFixtureResponse { Guid = guid, Status = ResponseStatus.Success };
        }
    }
}
