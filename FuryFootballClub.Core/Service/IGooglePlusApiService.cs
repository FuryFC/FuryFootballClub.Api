using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuryFootballClub.Core.Domain;

using Google.Apis.Auth.OAuth2.Responses;

namespace FuryFootballClub.Core.Service
{
    public interface IGooglePlusApiService
    {
        TokenResponse Connect(string code);
        UserProfile RetrieveProfile(TokenResponse state);
        void RevokeToken();
    }
}
