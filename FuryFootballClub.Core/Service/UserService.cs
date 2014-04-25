using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Repository;
using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Collections.Generic;

namespace FuryFootballClub.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // TODO: Use dependency injection here instead of this default constructor
        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Delete(Guid guid)
        {
            _userRepository.Delete(guid);
        }

        public IEnumerable<User> List()
        {
            return _userRepository.List();
        }

        public Guid Save(User user)
        {
            _userRepository.Save(user);
            return user.Id;
        }

        public User Find(Guid guid)
        {
            return _userRepository.FindByGuid(guid);
        }

        public User FindByAccessToken(string accessToken)
        {
            return _userRepository.FindByAccessToken(accessToken);
        }

        public User FindByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        public User CreateBasicUser(UserProfile userProfile)
        {
            var user = new User();
            user.setProfile(userProfile);
            // TODO: Set basic claims here
            return user;
        }

        public void Login(User user, TokenResponse state)
        {
            user.AccessToken = state.AccessToken;
            user.LastLogin = DateTime.UtcNow;
            _userRepository.Save(user);
        }
  
    }
}
