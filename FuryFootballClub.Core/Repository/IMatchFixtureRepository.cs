using FuryFootballClub.Core.Domain;
using System.Collections.Generic;

namespace FuryFootballClub.Core.Repository
{
    public interface IMatchFixtureRepository
    {
        /**
         * Bulk add operation
         */
        void Add(IEnumerable<MatchFixture> fixtures);

        /**
         * Save the item to the repository, if the item is new it will have a NULL Id
         */
        void Save(MatchFixture fixture);
        
        /**
         * Single Delete
         */
        void Delete(MatchFixture fixture);

        /**
         * Bulk retrieval
         */         
        IEnumerable<MatchFixture> List();

        /**
         * Find a single instance
         */
        MatchFixture FindByGuid(System.Guid guid);
    }
}
