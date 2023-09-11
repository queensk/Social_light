using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social_Light_Auth.Data;
using Social_Light_Auth.Extensions;
using Social_Light_Auth.Models;
using Social_Light_Auth.Models.DTO.JWToptions;
using Social_Light_Auth.Service;
using Social_Light_Auth.Service.IService;
using Social_light_Message_Bus.MessageBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// dependant injections
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJWtTokenGenerator, JwtService>();
builder.Services.AddScoped<IMessageBus, SocialMessageBus>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMigration();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
