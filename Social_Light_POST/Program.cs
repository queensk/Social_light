using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Social_Light_POST.Data;
using Social_Light_POST.Extensions;
using Social_Light_POST.Models.Config;
using Social_Light_POST.Services;
using Social_Light_POST.Services.IService;
using Social_Light_POST.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();

//Services
builder.Services.AddScoped<ICommentService, CommentsService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

//Cors policy
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
    // build.WithOrigins("https://localhost:7203", "http://localhost:7203");
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));

builder.Services.AddHttpClient("Comments", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentsApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("Users", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:UsersApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.Configure<AzureStorageConfig>(options => {
    options.AccountName =builder.Configuration.GetValue<string>("AzureStorageConfig:AccountName");
    options.AccountKey = builder.Configuration.GetValue<string>("AzureStorageConfig:AccountKey");
});



//custom builders
builder.AddSwaggerGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors("policy1");
app.MapControllers();

app.Run();
