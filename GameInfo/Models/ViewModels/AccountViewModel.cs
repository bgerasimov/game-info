using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel()
        {
            this.Roles = new List<string>();
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
