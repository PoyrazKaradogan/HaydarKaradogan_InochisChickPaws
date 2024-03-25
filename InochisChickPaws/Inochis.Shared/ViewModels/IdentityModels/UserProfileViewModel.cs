using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels.IdentityModels
{
    public class UserProfileViewModel
    {
        public UserViewModel User { get; set; }
        public List<AdminOrderViewModel> Orders { get; set; }
    }
}
