using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels.IdentityModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Boş bırakılamaz")]
        [DisplayName("Kullanıcı Adı:")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Boş bırakılamaz")]
        [DisplayName("Parolanız:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
