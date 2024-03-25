using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Inochis.Business.Abstract;
using Inochis.Business.Concrete;
using Inochis.Data.Abstract;
using Inochis.Data.Concrete.Contexts;
using Inochis.Data.Concrete.Repositories;
using Inochis.Entity.Concrete.Identity;
using Inochis.Shared.Helpers.Abstract;
using Inochis.Shared.Helpers.Concrete;
using Inochis.UI.EmailServices.Abstract;
using Inochis.UI.EmailServices.Concrete;
using Inochis.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<InochisDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<InochisDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    #region Parola Ayarlar�
    options.Password.RequiredLength = 6; //Parola en az 6 karakter olmal�
    options.Password.RequireDigit = true; //Parola say�sal de�er i�ermeli
    options.Password.RequireNonAlphanumeric = true;//Parola �zel karakter i�ermeli
    options.Password.RequireUppercase = true; //Parola b�y�k harf i�ermeli
    options.Password.RequireLowercase = true; //Parola k���k harf i�ermeli
                                              //options.Password.RequiredUniqueChars //Tekrar etmemesi istenen karakterler
    #endregion

    #region Hesap Kilitleme Ayarlar�
    options.Lockout.MaxFailedAccessAttempts = 3;//�st �ste hatal� giri� denemesi s�n�r�
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(50);//Kilitlenmi� bir hesaba yeniden giri� yapabilmek i�in gereken bekleme s�resi
    //options.Lockout.AllowedForNewUsers = true; //Yeniden kay�t olmaya imkan ver
    #endregion

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.SlidingExpiration = true;
    options.Cookie = new CookieBuilder
    {
        Name = "Inochis.Security.Cookie",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartManager>();
builder.Services.AddScoped<IShoppingCartItemService, ShoppingCartItemManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IImageHelper, ImageHelper>();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(options=>new SmtpEmailSender(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"]
  ));

builder.Services.AddNotyf(options =>
{
    options.DurationInSeconds = 3;
    options.IsDismissable = true;
    options.Position = NotyfPosition.TopRight;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UpdateDatabase().Run();
