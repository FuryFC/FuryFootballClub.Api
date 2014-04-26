using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuryFootballClub.Core.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        IUserRepository _userRepository;
        User _deletable;
        User _updateable;
        User _find1;
        User _find2;
        DateTime _today;
        IList<User> _users;

        [SetUp]
        public void SetUp()
        {
            _today = TimeZoneInfo.ConvertTimeToUtc(DateTime.Today);
            _userRepository = new UserRepository();

            /* Wipe out all match users from the test database */
            var users = _userRepository.List();
            foreach (var user in users)
            {
                _userRepository.Delete(user.Id);
            }

            /* Create some test data here */
            _deletable = new User() { PrimaryEmail = "offender@gmail.com", AccessToken = "BlahBlah", LastLogin = _today.AddDays(-1.0) };
            _updateable = new User() { PrimaryEmail = "defender@gmail.com" };
            _find1 = new User() { PrimaryEmail = "offender1@gmail.com", AccessToken = "BlahBlah2", LastLogin = _today.AddDays(-1.0) };
            _find2 = new User() { PrimaryEmail = "offender2@gmail.com" };
            _users = new List<User>();
            _users.Add(_deletable);
            _users.Add(_updateable);
            _users.Add(_find1);
            _users.Add(_find2);
            _userRepository.Add(_users);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var user in _users)
            {
                _userRepository.Delete(user.Id);
            }
        }

        #region Delete

        [Test]
        public void Delete_ByGuid()
        {
            Guid guid = _deletable.Id;

            Assert.IsNotNull(_userRepository.FindByGuid(guid));
            _userRepository.Delete(_deletable.Id);
            Assert.IsNull(_userRepository.FindByGuid(guid));
        }

        #endregion

        #region Retrieve

        private void AssertUserEqual(User target, User result)
        {
            Assert.AreEqual(target.Id, result.Id);
            Assert.AreEqual(target.PrimaryEmail, result.PrimaryEmail);
            Assert.AreEqual(target.AccessToken, result.AccessToken);
            Assert.AreEqual(target.LastLogin, result.LastLogin);
            Assert.AreEqual(target.Claims, result.Claims);
        }

        [Test]
        public void Retrieve_OneByGuid()
        {
            var result = _userRepository.FindByGuid(_find1.Id);
            AssertUserEqual(_find1, result);
        }

        [Test]
        public void Retrieve_OneByPrimaryEmail()
        {
            var result = _userRepository.FindByEmail(_find1.PrimaryEmail);
            AssertUserEqual(_find1, result);
        }

        [Test]
        public void Retrieve_OneByAccessToken()
        {
            var result = _userRepository.FindByAccessToken(_find1.AccessToken);
            AssertUserEqual(_find1, result);
        }


        [Test]
        public void Retrieve_All()
        {
            var guid1 = _find1.Id;
            var guid2 = _find2.Id;

            bool found1 = false;
            bool found2 = false;
            var allFixtures = _userRepository.List();
            foreach (var fixture in allFixtures)
            {
                if (guid1.Equals(fixture.Id))
                {
                    found1 = true;
                }
                if (guid2.Equals(fixture.Id))
                {
                    found2 = true;
                }
            }

            Assert.True(found1);
            Assert.True(found2);
        }

        #endregion

        #region Insert

        [Test]
        public void Insert_One()
        {
            var one = new User() { PrimaryEmail = "BabySeals2@gmail.com", AccessToken = "boohiss", LastLogin = _today.AddDays(3.0) };
            _users.Add(one);
            _userRepository.Save(one);

            var result = _userRepository.FindByGuid(one.Id);

            AssertUserEqual(one, result);
        }

        [Test]
        public void Insert_OneWithClaims()
        {
            var guid = Guid.NewGuid();
            var user = new User()
            {
                PrimaryEmail = "blah3.1@blah.com",
            };

            // TODO: WHY CAN'T WE ADD THE CLAIMS BEFORE THE FIRST SAVE?
            _users.Add(user);
            _userRepository.Save(user);

            user.Claims = new List<UserClaim>()
                {
                    new UserClaim() { Key = "ClaimKey1", Value = "ClaimValue1" },
                    new UserClaim() { Key = "ClaimKey2", Value = "ClaimValue2" }
                };
            _userRepository.Save(user);

            var result = _userRepository.FindByGuid(user.Id);

            AssertUserEqual(user, result);
        }

        [Test]
        public void Insert_Bulk()
        {
            var one = new User() { PrimaryEmail = "BabySeals3@gmail.com", AccessToken = "VH Dome, field 11", LastLogin = _today.AddDays(3.0) };
            var two = new User() { PrimaryEmail = "BabySeals4@gmail.com", AccessToken = "VH Dome, field 12", LastLogin = _today.AddDays(3.0) };
            _users.Add(one);
            _users.Add(two);
            _userRepository.Add(new[] { one, two });

            var result1 = _userRepository.FindByGuid(one.Id);
            var result2 = _userRepository.FindByGuid(two.Id);

            AssertUserEqual(one, result1);
            AssertUserEqual(two, result2);
        }

        #endregion

        #region Update
        [Test]
        public void Update_One()
        {
            Assert.AreEqual("defender@gmail.com", _updateable.PrimaryEmail);
            _updateable.PrimaryEmail = "defender2@gmail.com";
            _userRepository.Save(_updateable);

            var result = _userRepository.FindByGuid(_updateable.Id);

            AssertUserEqual(_updateable, result);
        }

        [Test]
        public void Update_OneRemoveClaim()
        {
            var guid = Guid.NewGuid();
            var user = new User()
            {
                PrimaryEmail = "blah3.8@blah.com",
            };

            _users.Add(user);
            _userRepository.Save(user);
            user.Claims = new List<UserClaim>()
                {
                    new UserClaim() { Key = "ClaimKey1", Value = "ClaimValue1" },
                    new UserClaim() { Key = "ClaimKey2", Value = "ClaimValue2" }
                };
            _userRepository.Save(user);
           

            var updateUser = _userRepository.FindByGuid(user.Id);

            _userRepository.DeleteClaimFromUser(updateUser.Claims.Where(o => o.Key == "ClaimKey1").FirstOrDefault());

            _userRepository.Save(updateUser);

            var result = _userRepository.FindByGuid(user.Id);

            Assert.AreEqual(1, user.Claims.Count);

            AssertUserEqual(user, result);
        }

        #endregion

        #region Exceptions
        // TODO: Add some exception tests
        #endregion
    }
}
