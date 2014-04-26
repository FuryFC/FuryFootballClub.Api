using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2.Responses;
using FuryFootballClub.Core.Service;
using System.Net;
using System.Collections.Generic;
using FuryFootballClub.Core.Domain;
using System.Security.Claims;
using System.Security;

namespace FuryFootballClub.Api.Auth
{
    public class OAuth2Handler : DelegatingHandler
    {
        static private string AuthToken = "X-Auth-Header";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var headers = request.Headers;
            if (headers.Contains(AuthToken))
            {
                var authJson = headers.GetValues(AuthToken).First();

                /* Grab the user from the access token */
                var auth = (TokenResponse)JsonConvert.DeserializeObject(authJson, typeof(TokenResponse));

                var userService = new UserService();
                var user = userService.FindByAccessToken(auth.AccessToken);
                if (user == null)
                {
                    var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                    response.ReasonPhrase = "Could not find user associated with that access token";
                    return Task.FromResult<HttpResponseMessage>(response);
                }

                // TODO: Check refresh token somewhere
                var claims = new List<Claim>();
                foreach(UserClaim claim in user.Claims)
                {
                    claims.Add(new Claim(claim.Key, claim.Value));
                }
                Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            }
            try
            {
                return base.SendAsync(request, cancellationToken);
            } catch(SecurityException e)
            {
                var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.ReasonPhrase = "You are not authorized for this action";
                return Task.FromResult<HttpResponseMessage>(response);
            }
        }
    }
}