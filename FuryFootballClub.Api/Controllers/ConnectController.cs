using FuryFootballClub.Api.Models.Connect;
using FuryFootballClub.Core.Service;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FuryFootballClub.Api.Controllers
{
    public class ConnectController : ApiController
    {
        private readonly IGooglePlusApiService _googlePlusService;
        private readonly IUserService _userService;

        // TODO: Add Dependency injection
        public ConnectController()
        {
            // create real google plus service here
            _googlePlusService = new GooglePlusApiService();
            _userService = new UserService();
        }

        public ConnectController(IGooglePlusApiService googlePlusApiService, IUserService userService)
        {
            _googlePlusService = googlePlusApiService;
            _userService = userService;
        }

        public HttpResponseMessage Post(NewConnectRequest newConnectRequest)
        {
            // First attempt to exchange the code and create a state
            var state = _googlePlusService.Connect(newConnectRequest.Code);

            // Grab the User's primary email address from Google
            var profile = _googlePlusService.RetrieveProfile(state);

            // Look up the user in the database
            var user = _userService.FindByEmail(profile.PrimaryEmail);
            
            // if the user does not exist, create the user
            if (user == null)
            {
                user = _userService.CreateBasicUser(profile);
            }

            // cache the state in the database for this user
            user.setProfile(profile);

            // now log in
            _userService.Login(user, state);

            // Return the state to the user
            var a = JsonConvert.SerializeObject(state); 
            return Request.CreateResponse(HttpStatusCode.OK, a);
        }
    }
}