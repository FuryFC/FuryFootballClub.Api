using System.Security.Claims;
using System.Linq;

namespace FuryFootballClub.Api.Auth
{
    public class CustomAuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            string resource = context.Resource.FirstOrDefault().Value;
            string action = context.Action.FirstOrDefault().Value;

            if (resource == "MatchFixture")
            {
                return context.Principal.HasClaim(ClaimTypes.Role, "TeamManager");
            }

            return false;
        }
    }
}