using FuryFootballClub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuryFootballClub.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context = new Context();

        /**
         * Bulk add operation
         */
        public void Add(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                if (user.Id.Equals(Guid.Empty)) user.Id = Guid.NewGuid();
            }
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        /**
         * Single Delete
         */
        public void Delete(Guid guid)
        {
            var matchFixture = _context.Users.Find(guid);
            /* Make sure it exists before deleting it */
            if (matchFixture != null)
            {
                _context.Users.Remove(matchFixture);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> List()
        {
            return _context.Users.ToList();
        }

        public User FindByGuid(System.Guid guid)
        {
            return _context.Users.Find(guid);
        }

        public User FindByAccessToken(string accessToken)
        {
            // Query for the Blog named ADO.NET Blog 
           return _context.Users
                            .Where(u => u.AccessToken == accessToken)
                            .FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            // Query for the Blog named ADO.NET Blog 
            return _context.Users
                             .Where(u => u.PrimaryEmail == email)
                             .FirstOrDefault();
        }


        public void Save(User user)
        {
            if (user.Id.Equals(Guid.Empty))
            {
                user.Id = Guid.NewGuid();
                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Attach(user);
            }
            _context.SaveChanges();
        }

    }
}
