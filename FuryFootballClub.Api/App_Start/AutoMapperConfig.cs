using AutoMapper;
using FuryFootballClub.Api.Models;
using FuryFootballClub.Core.Domain;

namespace FuryFootballClub.Api
{
    public class AutoMapperConfig
    {
        public void RegisterModelMappings()
        {
            Mapper.CreateMap<MatchFixture, NewMatchFixtureRequest>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}