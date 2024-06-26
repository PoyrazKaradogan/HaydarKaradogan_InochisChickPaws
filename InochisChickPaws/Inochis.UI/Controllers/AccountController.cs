﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Inochis.Business.Abstract;
using Inochis.Entity.Concrete.Identity;
using Inochis.Shared.ViewModels;
using Inochis.Shared.ViewModels.IdentityModels;
using Inochis.UI.EmailServices.Abstract;

namespace Inochis.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOrderService _orderManager;
        private readonly IEmailSender _emailSender;
        private readonly IShoppingCartService _shoppingCartManager;
        private readonly INotyfService _notyfService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IOrderService orderManager, IEmailSender emailSender, IShoppingCartService shoppingCartManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _orderManager = orderManager;
            _emailSender = emailSender;
            _shoppingCartManager = shoppingCartManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    EmailConfirmed=true
                };

                var result = await _userManager.CreateAsync(user,registerViewModel.Password);
                if (result.Succeeded)
                {
        
                    //var tokenCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var backUrl = Url.Action("ConfirmEmail", "Account", new
                    //{
                    //    userId= user.Id,
                    //    token=tokenCode
                    //});

                    //Mail gönderme
                    //await _emailSender.SendEmailAsync(
                    //    user.Email,
                    //    "InochisApp Üyelik Onayı",
                    //    $"<p>InochisApp uygulamasına üyeliğinizi onaylamak için aşağıdaki linke tıklayınız.</p><a href='https://localhost:59079{backUrl}'>ONAY LİNKİ</a>"
                    //    );

                    //Yukarıdaki email onayı kodlarını aktif ettiğimizde burayı sileceğiz.
                    await _shoppingCartManager.InitializeShoppingCartAsync(user.Id);

                    _notyfService.Success("Üyeliğiniz başarıyla oluşturulmuştur. Mailinizi kontrol ederek üyeliğinizi onaylayabilirsiniz.",10);
                    return Redirect("~/");
                }

            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user != null)
                {
                   
                    await _signInManager.SignOutAsync();
                 
                    var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                    if (!isConfirmed)
                    {
                        _notyfService.Warning("Hesabınız onaylı değildir. Mailinize gelen onay linkini tıklayarak, onaylayabilirsiniz.");
                        return View(loginViewModel);
                    }
                    //Login olmayı deniyoruz.
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);
                    var failedAttempCount = await _userManager.GetAccessFailedCountAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        var returnUrl = TempData["ReturnUrl"]?.ToString();
                        _notyfService.Custom(" Hoş geldiniz. Hesabınıza erişim sağladınız.");
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else if (!result.IsLockedOut)
                    {
                        var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);
                        if(lockoutEndDate != null)
                        {
                            var timeLeft = (lockoutEndDate.Value - DateTime.Now).Seconds;
                            if (timeLeft == _userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalSeconds){
                                _notyfService.Custom($"Hesabınız kilitlenmiştir. Lütfen {lockoutEndDate} zamanından sonra deneyiniz.");
                            }
                        }
                        else
                            {
                                var attempCount = _signInManager.Options.Lockout.MaxFailedAccessAttempts;
                                var accessFailedCount = attempCount - failedAttempCount;
                                _notyfService.  Custom($"Kalan deneme hakkınız: {accessFailedCount}");
                            }
                        
                    }
                    else
                    {
                        var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = (lockoutEndDate.Value - DateTime.Now).Seconds;
                        _notyfService.Custom($"Hesabınız kilitli. Lütfen {timeLeft} sn sonra yeniden deneyiniz.");
                        return View(loginViewModel);
                    }
                }

            }
            return View(loginViewModel);
        }
    
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["ReturnUrl"] = null;
            _notyfService.Custom("Çıkış başarılı, güle güle");
            return Redirect("~/");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var orders = await _orderManager.GetOrdersAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);
                
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel
            {
                User = new UserViewModel
                {
                    Id= userId,
                    FirstName = user.FirstName,
                    LastName= user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    City = user.City,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth
                },
                Orders = orders
            };
            return View(userProfileViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel userProfileViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                user.FirstName = userProfileViewModel.User.FirstName;
                user.LastName= userProfileViewModel.User.LastName;
                user.Email=userProfileViewModel.User.Email;
                user.City = userProfileViewModel.User.City;
                user.Address = userProfileViewModel.User.Address;
                user.PhoneNumber = userProfileViewModel.User.PhoneNumber;
                user.DateOfBirth=userProfileViewModel.User.DateOfBirth;
                user.Gender=userProfileViewModel.User.Gender;
                user.UserName=userProfileViewModel.User.UserName;
                
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, false);
                    _notyfService.Custom("Profiliniz başarıyla güncellenmiştir");
                    return Redirect("~/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            userProfileViewModel.Orders = await _orderManager.GetOrdersAsync(userId);
            return View(userProfileViewModel);
        }

 
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
                var isVerified = await _userManager.CheckPasswordAsync(user, changePasswordViewModel.OldPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user,changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, changePasswordViewModel.NewPassword, false, false);
                        _notyfService.Custom("Şifreniz başarıyla değiştirilmiştir.");
                        return RedirectToAction("Profile");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordViewModel);
                }
                _notyfService.Warning("Geçerli şifreniz hatalıdır");
            }
            return View(changePasswordViewModel);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                _notyfService.Custom("Bilgilerinizde hata var. Kontrol ediniz.");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _notyfService.Custom("Kullanıcı bilgilerinize ulaşılamadı.");
                return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded)
            {
                //Bu kişi onaylı bir user olduğuna göre, bir shoppingcart oluşturuyoruz.
                await _shoppingCartManager.InitializeShoppingCartAsync(userId);
                _notyfService.Information("Hesabınız başarıyla onaylanmıştır.");
                return RedirectToAction("Login");
            }
            _notyfService.Custom("Hesabınız onaylanırken bir sorun oluştu, daha sonra tekrar deneyiniz.");
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if(email == null)
            {
                ModelState.AddModelError("", "Mail adresinizi yazınız.");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                ModelState.AddModelError("", "Böyle bir hesap bulunamadı.");
                return View();
            }
            var tokenCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var backUrl = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                tokenCode = tokenCode
            });
            var subject = "InochisApp Şifre Sıfırlama";
            var htmlMessage = $"<h1>InochisApp Şifre Sıfırlama İşlemi</h1>" +
                $"<p>" +
                $"Lütfen şifrenizi sıfırlamak için aşağıdaki linke tıklayınız." +
                $"</p>" +
                $"<a href='https://localhost:59079{backUrl}'>Şifre Sıfırla</a>";
            await _emailSender.SendEmailAsync(email,subject,htmlMessage);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ResetPassword(string userId, string tokenCode)
        {
            if(userId==null || tokenCode==null)
            {
                ModelState.AddModelError("", "Bir sorun oluştu");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı");
                return View();
            }
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel
            {
                UserId = userId,
                TokenCode = tokenCode
            };
            return View(resetPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByIdAsync(resetPasswordViewModel.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı");
                return View(resetPasswordViewModel);
            }
            var result = await _userManager.ResetPasswordAsync(
                user, 
                resetPasswordViewModel.TokenCode, 
                resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordViewModel);
        }
    }
}
