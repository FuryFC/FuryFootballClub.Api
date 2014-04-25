using FuryFootballClub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuryFootballClub.Core.Repository
{
    public class MatchFixtureRepository : IMatchFixtureRepository
    {
        private readonly Context _context = new Context();

        /**
         * Bulk add operation
         */
        public void Add(IEnumerable<MatchFixture> fixtures)
        {
            foreach (var fixture in fixtures)
            {
                if (fixture.Id.Equals(Guid.Empty)) fixture.Id = Guid.NewGuid();
            }
            _context.MatchFixtures.AddRange(fixtures);
            _context.SaveChanges();
        }

        /**
         * Single Delete
         */
        public void Delete(Guid guid)
        {
            var matchFixture = _context.MatchFixtures.Find(guid);
            /* Make sure it exists before deleting it */
            if (matchFixture != null)
            {
                _context.MatchFixtures.Remove(matchFixture);
                _context.SaveChanges();
            }
        }

        public IEnumerable<MatchFixture> List()
        {
            return _context.MatchFixtures.ToList();
        }

        public MatchFixture FindByGuid(System.Guid guid)
        {
            return _context.MatchFixtures.Find(guid);
        }

        public void Save(MatchFixture fixture)
        {
            if (fixture.Id.Equals(Guid.Empty))
            {
                fixture.Id = Guid.NewGuid();
                _context.MatchFixtures.Add(fixture);
            }
            else
            {
                _context.MatchFixtures.Attach(fixture);
            }
            _context.SaveChanges();
        }

    }
}
