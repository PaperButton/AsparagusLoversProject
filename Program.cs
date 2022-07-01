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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ILover, AsparagusLover>();
builder.Services.AddTransient<IFoodIntakeCounter, FoodIntakeCounter>();
builder.Services.AddScoped<AsparagusLoverRepository<ILover>>();
builder.Services.AddScoped<IFoodIntakeCounterRepository<IFoodIntakeCounter,ILover>, FoodIntakeCounterRepository>();

builder.Services.AddScoped<PrepareNewLoverForValidatingAttribute>();

IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSession();
builder.Services.AddMvc();

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
