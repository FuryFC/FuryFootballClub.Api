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
            var user = _context.Users.Find(guid);
            /* Make sure it exists before deleting it */
            if (user != null)
            {
                if (user.Claims != null && user.Claims.Count > 0)
                {
                    _context.UserClaims.RemoveRange(user.Claims.ToList());
                    _context.SaveChanges();
                    user.Claims = new List<UserClaim>();
                }
                _context.Users.Remove(user);
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


        /**
         * Note: Do not just remove claims, use the DeleteClaimFromUser method for that, otherwise it will not work
         */
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

            /* 
             * Deal with claims here because they really are a sub-class
             */
            if (user.Claims != null)
            {
                foreach (UserClaim claim in user.Claims)
                {
                    if (claim.User == null)
                    {
                        _context.UserClaims.Add(claim);
                        claim.User = user;
                    }
                    else
                    {
                        _context.UserClaims.Attach(claim);
                    }
                }
            }

            _context.SaveChanges();
        }

        public void DeleteClaimFromUser(UserClaim userClaim)
        {
            if (userClaim.User == null) userClaim.User = _context.Users.Find(userClaim.UserId);
            userClaim.User.Claims.Remove(userClaim);
            userClaim.User = null;
            _context.UserClaims.Remove(userClaim);
            _context.SaveChanges();
        }
    }
}
