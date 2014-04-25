using FuryFootballClub.Core.Domain;
using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Collections.Generic;

namespace FuryFootballClub.Core.Service
{
    public interface IUserService
    {
        void Delete(Guid guid);
        IEnumerable<User> List();
        Guid Save(User user);
        User Find(Guid guid);
        User FindByAccessToken(string accessToken);
        User FindByEmail(string email);
        User CreateBasicUser(UserProfile userProfile);
        void Login(User user, TokenResponse state);
    }
}
