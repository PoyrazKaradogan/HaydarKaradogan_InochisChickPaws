using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels.IdentityModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public List<AssignRoleViewModel> Roles { get; set; }
    }
}
