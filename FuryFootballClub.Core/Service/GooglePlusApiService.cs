using FuryFootballClub.Core.Domain;
using Google.Apis.Auth.OAuth2;
// Generated libraries for Google APIs
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Plus.v1;
using System;
using System.IO;
using System.Threading;


namespace FuryFootballClub.Core.Service
{
    public class GooglePlusApiService : IGooglePlusApiService
    {
        /// <summary>Retrieves the Client Configuration from the server path.</summary>
        /// <returns>Client secrets that can be used for API calls.</returns>
        public static GoogleClientSecrets GetClientConfiguration()
        {
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                return GoogleClientSecrets.Load(stream);
            }
        }

        /* TODO: Make connect return a non-google specific type so that we can implement facebook logins too, and maybe our own proprietary eventually */
        public TokenResponse Connect(string code)
        {
            /* This is straight out of examples online.  The Google+ sign-in button gives us a code
             * we need to exchange that code for authorization tokens.  We also will then cache those
             * in our local database so that we can just refresh them in the future
             */

            /* First step is to create an authorization flow */
            var flow =
                new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = GetClientConfiguration().Secrets,
                    Scopes = new string[] { PlusService.Scope.PlusLogin }
                });

            /* Second step is to use that flow to fetch a token from google.apis */
            var response = flow.ExchangeCodeForTokenAsync("", code, "postmessage", CancellationToken.None).Result; // response.accessToken now should be populated

            return response;
        }

        public UserProfile RetrieveProfile(TokenResponse token)
        {
            /* 
             * I (cmm) stole this code from online resources, so hard to explain what it is doing, but I'll give it my best
             */

            /* First step, create a flow, using the pluslogin scope, may need to extend this beyond just that */
            IAuthorizationCodeFlow flow =
                new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = GetClientConfiguration().Secrets,
                    Scopes = new string[] { PlusService.Scope.PlusLogin }
                });

            /* Now that we have a flow, we can create a user credential */
            UserCredential credential = new UserCredential(flow, "me", token);

            /* We can now use that credential to create our link to google plus */
            var plusService = new PlusService(
                new Google.Apis.Services.BaseClientService.Initializer()
                {
                    ApplicationName = "Fury FC",
                    HttpClientInitializer = credential
                });

            /* Sweet, we are connected!  Now we can retrieve the profile information and return it */
            var plusProfile = plusService.People.Get("me").Execute();

            return new UserProfile()
            {
                PrimaryEmail = plusProfile.Emails[0].Value,
                AboutMe = plusProfile.AboutMe,
                DisplayName = plusProfile.DisplayName,
                ImageUrl = plusProfile.Image.Url,
                Tagline = plusProfile.Tagline
            };
        }

        public void RevokeToken()
        {
            throw new NotImplementedException("Not Implemented Yet");
        }

    }
}
