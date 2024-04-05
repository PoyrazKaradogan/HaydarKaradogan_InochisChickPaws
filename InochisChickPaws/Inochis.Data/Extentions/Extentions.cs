using Inochis.Entity.Concrete.Identity;
using Inochis.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Inochis.Entity.Concrete;
using Inochis.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            #region Rol Bilgileri

            List<Role> roles = new List<Role>
            {
                new Role{Name="SuperAdmin", Description="Süper Yönetici haklarını taşıyan rol", NormalizedName="SUPERADMIN"},
                new Role{Name="Admin", Description="Yönetici haklarını taşıyan rol", NormalizedName="ADMIN"},
                new Role{Name="Customer", Description="Müşteri haklarını taşıyan rol", NormalizedName="CUSTOMER"}
            };
            modelBuilder.Entity<Role>().HasData(roles);

            #endregion

            #region Kullanıcı Bilgileri

            List<User> users = new List<User>
            {
                new User
                {
                    FirstName="Poyraz",
                    LastName="Karadoğan",
                    UserName="poyrazkaradogan",
                    NormalizedUserName="POYRAZKARADOGAN",
                    Email="poyrazkaradogan@gmail.com",
                    NormalizedEmail="POYRAZKARADOGAN@GMAIL.COM",
                    Gender="Erkek",
                    DateOfBirth=new DateTime(1990,5,12),
                    Address="Ataşehir Residence No:!6/12",
                    City="İstanbul",
                    PhoneNumber="5427643469",
                    EmailConfirmed=true
                },
         
            };

            modelBuilder.Entity<User>().HasData(users);
            #endregion

            #region Şifre İşlemleri

            var passwordHasher = new PasswordHasher<User>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Poyraz123.");

            #endregion

            #region Rol Atama İşlemleri
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                     UserId=users[0].Id,
                     RoleId=roles[0].Id
                   
                }
              
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion

            #region Alışveriş Sepeti İşlemleri

            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>
            {
                new ShoppingCart{ Id=1, UserId=users[0].Id }
            
            };
            modelBuilder.Entity<ShoppingCart>().HasData(shoppingCarts);

            #endregion
        }
    }
}
