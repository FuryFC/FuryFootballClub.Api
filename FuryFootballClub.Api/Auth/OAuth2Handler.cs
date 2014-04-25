using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


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
                string auth = headers.GetValues(AuthToken).First();
                // Carlos, you were here, about to create a claims principal after pulling user info from google+
            }
//            HttpContextBase httpContext;
//            string userName;
//           HashSet<string> scope;
            //       if (!request.TryGetHttpContext(out httpContext))
            //         throw new InvalidOperationException(“HttpContext must not be null.”);

            // var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(signing,encrypting));
            //   var error = resourceServer.VerifyAccess(httpContext.Request, out userName, out scope);
            // if (error != null)
            //   return Task<HttpResponseMessage>.Factory.StartNew(error.ToHttpResponseMessage);
            //   var identity = new ClaimsIdentity(scope.Select(s => new Claim(s, s)));
            //    if (!string.IsNullOrEmpty(userName))
            //         identity.Claims.Add(new Claim(ClaimTypes.Name, userName));
            //    httpContext.User = ClaimsPrincipal.CreateFromIdentity(identity);
            //    Thread.CurrentPrincipal = httpContext.User;
            return base.SendAsync(request, cancellationToken);
        }
    }
}