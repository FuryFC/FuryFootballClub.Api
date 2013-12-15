using System.Web.Http;
using AutoMapper;
using FuryFootballClub.Api.Models;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Service;

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

        public MatchFixtureResponse Post([FromBody]MatchFixtureDto matchFixtureDto)
        {
            if (matchFixtureDto == null) { return new MatchFixtureResponse() {Status = ResponseStatus.Failure}; }

            var matchFixture = _mapper.Map<MatchFixtureDto, MatchFixture>(matchFixtureDto);
            var guid = _matchFixtureService.Save(matchFixture);

            return new MatchFixtureResponse {Guid = guid, Status = ResponseStatus.Success};
        }
    }
}
