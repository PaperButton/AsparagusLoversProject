using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using AsparagusLoversProject.Domain;
using System.Web.Mvc;
using AsparagusLoversProject.Repositories;
using Xunit;
using AsparagusLoversProject.Filters;
using System.Configuration;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ILover, AsparagusLover>();
builder.Services.AddTransient<IFoodIntakeCounter, FoodIntakeCounter>();
builder.Services.AddScoped<AsparagusLoverRepository<ILover>>();
builder.Services.AddScoped<IFoodIntakeCounterRepository<IFoodIntakeCounter,ILover>, FoodIntakeCounterRepository>();

builder.Services.AddScoped<PrepareNewLoverForValidatingAttribute>();

builder.Services.AddScoped<ApplicationUser>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                                            .AddIdentity<ApplicationUser, ApplicationRole>(config =>
                                            {
                                                config.Password.RequireDigit = false;
                                                config.Password.RequireLowercase = false;
                                                config.Password.RequireNonAlphanumeric = false;
                                                config.Password.RequireUppercase = false;
                                                config.Password.RequiredLength = 6;
                                            })
                .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddSession();
builder.Services.AddMvc();


builder.Services.AddAuthentication()
    .AddOAuth("VK", "VKontakte", config =>
    {
        config.ClientId = configuration["Authentication:VK:AppId"];
        config.ClientSecret = configuration["Authentication:VK:AppSecret"];
        config.ClaimsIssuer = "VKontakte";
        config.CallbackPath = new PathString("/signin-vkontakte-token");
        config.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
        config.TokenEndpoint = "https://oauth.vk.com/access_token";
        config.Scope.Add("email");
        config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
        config.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        config.SaveTokens = true;
        config.Events = new OAuthEvents
        {
            OnCreatingTicket = context =>
            {
                context.RunClaimActions(context.TokenResponse.Response.RootElement);
                return Task.CompletedTask;
            },
            OnRemoteFailure = context =>
            {
                Console.WriteLine(context.Failure);
                return Task.CompletedTask;
            }
        };
    });


builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Admin/Login";
    config.AccessDeniedPath = "/Home/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Administrator");
    });

    options.AddPolicy("Manager", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager")
                                      || x.User.HasClaim(ClaimTypes.Role, "Administrator"));
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "newsFeed",
    pattern: "/{controller=NewsFeed}",
    new {controller = "NewsFeed", action = "ShowNewsFeed"}
    );



app.Run();
