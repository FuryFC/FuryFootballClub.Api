using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Collections.Generic;

namespace FuryFootballClub.Core.Domain
{
    // TODO: convert to strongly typed
    public class User
    {
        public Guid Id { get; set; }

        public string PrimaryEmail { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual ICollection<UserClaim> Claims { get; set; }

        /* Token information, todo: consider giving this its own table */
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenScope { get; set; }

        public string TokenType { get; set; }

        public DateTime? TokenIssued { get; set; }

        public long? TokenExpiresInSeconds { get; set; }

        #region Special Setters

        public void setProfile(UserProfile profile)
        {
            PrimaryEmail = profile.PrimaryEmail;
        }

        public void setToken(TokenResponse token)
        {
            AccessToken = token.AccessToken;
            RefreshToken = token.RefreshToken;
            TokenExpiresInSeconds = token.ExpiresInSeconds;
            TokenScope = token.Scope;
            TokenIssued = token.Issued = DateTime.UtcNow;
            TokenType = token.TokenType;
        }

        public TokenResponse getToken()
        {
            return this.AccessToken == null ? null : new TokenResponse()
            {
                AccessToken = this.AccessToken,
                RefreshToken = this.RefreshToken,
                ExpiresInSeconds = TokenExpiresInSeconds.Value,
                Scope = this.TokenScope,
                Issued = this.TokenIssued.Value,
                TokenType = this.TokenType
            };
        }

        #endregion
    }
}
