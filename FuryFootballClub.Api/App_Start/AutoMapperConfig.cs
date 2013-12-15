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
            Mapper.CreateMap<MatchFixture, MatchFixtureData>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}