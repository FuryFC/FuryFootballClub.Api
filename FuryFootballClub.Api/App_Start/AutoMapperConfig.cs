using AutoMapper;
using FuryFootballClub.Api.Models.MatchFixture;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Api
{
    public class AutoMapperConfig
    {
        public void RegisterModelMappings()
        {
            Mapper.CreateMap<GetMatchFixtureRequest, MatchFixture>();
            Mapper.CreateMap<NewMatchFixtureRequest, MatchFixture>();
            Mapper.CreateMap<UpdateMatchFixtureRequest, MatchFixture>();
            Mapper.CreateMap<MatchFixture, MatchFixtureData>().ForMember(d => d.Guid, o => o.MapFrom(s => s.Guid));

            Mapper.AssertConfigurationIsValid();
        }
    }
}