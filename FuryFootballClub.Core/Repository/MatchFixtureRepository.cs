using FuryFootballClub.Core.Domain;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuryFootballClub.Core.Repository
{
    public class MatchFixtureRepository : MongoRepository<MatchFixture, Guid>, IMatchFixtureRepository
        
    {
        public IEnumerable<MatchFixture> List()
        {
            return Collection.FindAll();
        }

        public MatchFixture FindByGuid(System.Guid guid)
        {
            return GetById(guid);
        }

        public void Save(MatchFixture fixture)
        {
            Collection.Save(fixture);
        }
    }
}
