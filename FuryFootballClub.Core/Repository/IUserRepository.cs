using FuryFootballClub.Core.Domain;
using System.Collections.Generic;
using System;

namespace FuryFootballClub.Core.Repository
{
    public interface IUserRepository
    {
        /**
         * Bulk add operation
         */
        void Add(IEnumerable<User> users);

        /**
         * Save the item to the repository, if the item is new it will have a NULL Id
         */
        void Save(User user);
        
        /**
         * Single Delete
         */
        void Delete(Guid guid);

        /**
         * Bulk retrieval
         */         
        IEnumerable<User> List();

        /**
         * Find a single instance
         */
        User FindByGuid(Guid guid);

        /**
         * Find a single instance
         */
        User FindByAccessToken(string accessToken);

        /**
         * Find a single instance
         */
        User FindByEmail(string email);
    }
}
