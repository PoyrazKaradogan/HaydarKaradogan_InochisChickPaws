using Inochis.Shared.ViewModels.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels
{
    public class MessageViewModel
    {
        public MessageDetailsViewModel MessageDetails { get; set; }
        public MessageViewModel() { }

        public List<UserViewModel> UserList { get; set; }
    }
}
