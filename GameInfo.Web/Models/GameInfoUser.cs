using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class GameInfoUser : IdentityUser
    {
        public GameInfoUser()
        {
            this.Guides = new HashSet<Guide>();
        }

        public string AvatarUrl { get; set; }

        public ICollection<Guide> Guides { get; set; }
    }
}
