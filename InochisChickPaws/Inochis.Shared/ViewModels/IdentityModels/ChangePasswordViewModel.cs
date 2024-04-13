﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels.IdentityModels
{
    public class ChangePasswordViewModel
    {
        [DisplayName("Eski şifreniz:")]
        [Required(ErrorMessage ="{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DisplayName("Yeni şifreniz:")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni şifreniz tekrar:")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Şifreler uyuşmuyor!")]
        public string ReNewPassword { get; set; }
    }
}
