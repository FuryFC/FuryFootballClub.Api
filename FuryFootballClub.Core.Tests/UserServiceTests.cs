using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Repository;
using FuryFootballClub.Core.Service;
using Google.Apis.Auth.OAuth2.Responses;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace FuryFootballClub.Core.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userRepository = MockRepository.GenerateMock<IUserRepository>();
            _userService = new UserService(_userRepository);
        }

        #region Delete
        [Test]
        public void Delete_One()
        {
            var userGuid = Guid.NewGuid();

            _userRepository.Expect(s => s.Delete(userGuid));

            _userService.Delete(userGuid);
            _userRepository.VerifyAllExpectations();
        }

        #endregion

        #region List
        [Test]
        public void List_All()
        {
            var users = new[] 
            { 
                new User() { Id = Guid.NewGuid(), PrimaryEmail="blah@blah.com" },
                new User() { Id = Guid.NewGuid(), PrimaryEmail="blah2@blah.com" }
            };

            _userRepository.Expect(s => s.List()).Return(users);

            var result = _userService.List();

            Assert.AreSame(users,result);
            _userRepository.VerifyAllExpectations();
        }
        #endregion

        #region Save
        [Test]
        public void Save_One()
        {
            var user = new User() { Id = Guid.NewGuid(), PrimaryEmail = "blah3@blah.com" };

            _userRepository.Expect(s => s.Save(user));

            var guid = _userService.Save(user);

            Assert.AreEqual(user.Id, guid);
            _userRepository.VerifyAllExpectations();
        }

        #endregion

        #region CreateBasicUser
        [Test]
        public void CreateBasicUser()
        {
            var userProfile = new UserProfile() { PrimaryEmail = "blah4@blah.com" };

            var user = _userService.CreateBasicUser(userProfile);

            // TODO: Check for appropriate basic claims here

            Assert.AreEqual(userProfile.PrimaryEmail, user.PrimaryEmail);
        }
        #endregion

        #region Login
        [Test]
        public void User_Login()
        {
            var user = new User() { Id = Guid.NewGuid(), PrimaryEmail = "blah5@blah.com" };

            var state = new TokenResponse() { AccessToken = "hello" };

            var oldDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Today).AddDays(-1);

            _userRepository.Expect(s => s.Save(user));

            _userService.Login(user, state);

            Assert.AreEqual(state.AccessToken, user.AccessToken);

            Assert.Greater(user.LastLogin, oldDate);

            _userRepository.VerifyAllExpectations();
        }
        #endregion

        #region Find
        [Test]
        public void Find_One()
        {
            var userGuid = Guid.NewGuid();

            _userRepository.Expect(s => s.FindByGuid(userGuid));

            _userService.Find(userGuid);
            _userRepository.VerifyAllExpectations();
        }

        [Test]
        public void Find_OneEmail()
        {
            var email = "email@email.com";

            _userRepository.Expect(s => s.FindByEmail(email));

            _userService.FindByEmail(email);
            _userRepository.VerifyAllExpectations();
        }

        [Test]
        public void Find_OneToken()
        {
            var token = "blah";

            _userRepository.Expect(s => s.FindByAccessToken(token));

            _userService.FindByAccessToken(token);
            _userRepository.VerifyAllExpectations();
        }

        #endregion
    }
}
