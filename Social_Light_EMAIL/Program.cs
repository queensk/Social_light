using Microsoft.EntityFrameworkCore;
using Social_Light_EMAIL.Data;
using Social_Light_EMAIL.Messaging;
using Social_Light_EMAIL.Services;
using Social_Light_EMAIL.Extensions;
using Social_Light_EMAIL.Messaging.IMessaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();
dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
builder.Services.AddSingleton(new EmailService(dbContextBuilder.Options));

builder.Services.AddSingleton<IAzureMessageBusConsumer, AzureMessageBusConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();
app.useAzure();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
