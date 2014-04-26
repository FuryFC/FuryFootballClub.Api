using FuryFootballClub.Api.Controllers;
using FuryFootballClub.Api.Models.Connect;
using FuryFootballClub.Core.Domain;
using FuryFootballClub.Core.Service;
using Google.Apis.Auth.OAuth2.Responses;
using Newtonsoft.Json;
using NUnit.Framework;
using Rhino.Mocks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace FuryFootballClub.Api.Tests.Controllers
{
    [TestFixture]
    public class ConnectControllerTest
    {
        private IGooglePlusApiService _googlePlusApiService;
        private IUserService _userService;
        private ConnectController _controller;

        [SetUp]
        public void SetUp()
        {
            _googlePlusApiService = MockRepository.GenerateMock<IGooglePlusApiService>();
            _userService = MockRepository.GenerateMock<IUserService>();

            _controller = new ConnectController(_googlePlusApiService, _userService);

            // Mocking Context for the controller
            var request = MockRepository.GenerateMock<HttpRequestMessage>();
            _controller.Request = request;
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region Delete

        [Test]
        public void Delete_RemoveExistingConnectionToGooglePlus()
        {
//            _ConnectService.Expect(s => s.Delete(ConnectGuid));

//            var result = _controller.Delete(ConnectGuid);

//            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        #endregion

 

        #region Post

        [Test]
        public void Post_ConnectToGooglePlusNewUser()
        {
            var code = "SomeRandomCode";
            var newConnectRequest = new NewConnectRequest() { Code = code };

            var authState = new TokenResponse(){ AccessToken = "Hello",
                                                 RefreshToken = "GoodBye" };

            var userProfile = new UserProfile()
            {
                PrimaryEmail = "bob@gmail.com",
                AboutMe = "About me",
                DisplayName = "Bob Costas",
                ImageUrl = "https://google+.com/myimage",
                Tagline = "Let's party!!"
            };

            var user = new User()
            {
                PrimaryEmail = userProfile.PrimaryEmail,
            };

            _googlePlusApiService.Expect(s => s.Connect(code)).Return(authState);
            
            _googlePlusApiService.Expect(s => s.RetrieveProfile(authState)).Return(userProfile);

            _userService.Expect(s => s.FindByEmail(userProfile.PrimaryEmail)).Return(null);

            _userService.Expect(s => s.CreateBasicUser(userProfile)).Return(user);

            _userService.Expect(s => s.Login(user, authState));

            var result = _controller.Post(newConnectRequest);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            
            var resultState = ((ObjectContent)result.Content).ReadAsAsync<TokenResponse>().Result;

            Assert.AreEqual(authState.AccessToken, resultState.AccessToken);
            Assert.AreEqual(authState.RefreshToken, resultState.RefreshToken);

            _googlePlusApiService.VerifyAllExpectations();
            _userService.VerifyAllExpectations();
        }

        [Test]
        public void Post_ConnectToGooglePlusExistingUser()
        {
            var code = "SomeRandomCode";
            var newConnectRequest = new NewConnectRequest() { Code = code };

            var authState = new TokenResponse()
            {
                AccessToken = "Hello",
                RefreshToken = "GoodBye"
            };

            var userProfile = new UserProfile()
            {
                PrimaryEmail = "bob@gmail.com",
                AboutMe = "About me",
                DisplayName = "Bob Costas",
                ImageUrl = "https://google+.com/myimage",
                Tagline = "Let's party!!"
            };

            var user = new User()
            {
                PrimaryEmail = "junk@gmail.com",
            };

            _googlePlusApiService.Expect(s => s.Connect(code)).Return(authState);

            _googlePlusApiService.Expect(s => s.RetrieveProfile(authState)).Return(userProfile);

            _userService.Expect(s => s.FindByEmail(userProfile.PrimaryEmail)).Return(user);

            _userService.Expect(s => s.Login(user, authState));

            var result = _controller.Post(newConnectRequest);

            Assert.AreEqual(userProfile.PrimaryEmail, user.PrimaryEmail);

            _googlePlusApiService.VerifyAllExpectations();

            _userService.VerifyAllExpectations();
        }


        #endregion
    }
}
