using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuryFootballClub.Core.Domain
{
    public class UserClaim
    {
        [Key, Column(Order = 0)]
        public Guid UserId { get; set;  }
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
