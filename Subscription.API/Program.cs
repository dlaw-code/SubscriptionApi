using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Subscription.API.Extension;
using Subscription.API.Service.Implementation;
using Subscription.API.Service.Interface;
using Subscription.DATA.Context;
using Subscription.DATA.Repository.Implementation;
using Subscription.DATA.Repository.Interface;
using Subscription.MODEL.Entities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureLibrary(builder.Configuration);

builder.Services.AddIdentity<ServiceUser, IdentityRole>().AddEntityFrameworkStores<SubscriptionContextDb>().AddDefaultTokenProviders();
builder.Services.AddDbContext<SubscriptionContextDb>(dbContextOptions => dbContextOptions.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddScoped<IAccountRepo, AccountRepo>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGenerateJwt, GenerateJwt>();
builder.Services.AddScoped<ISubscriptionRepo, SubscriptionRepo>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();
/*}*/

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
