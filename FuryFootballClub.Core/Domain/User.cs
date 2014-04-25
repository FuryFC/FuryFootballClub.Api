using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuryFootballClub.Core.Domain
{
    // TODO: convert to strongly typed
    public class User
    {
        public Guid Id { get; set; }

        public string AccessToken { get; set; }

        public string PrimaryEmail { get; set; }

        public DateTime? LastLogin { get; set; }

        public void setProfile(UserProfile profile)
        {
            PrimaryEmail = profile.PrimaryEmail;
        }
    }
}
