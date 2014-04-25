using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuryFootballClub.Core.Domain
{
    public class UserProfile
    {
        public string PrimaryEmail { get; set; }
        public string ImageUrl { get; set; }
        public string DisplayName { get; set; }
        public string Tagline { get; set; }
        public string AboutMe { get; set; }
    }
}
