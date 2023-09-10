using Microsoft.EntityFrameworkCore;
using Social_Light_POST.Data;
using Social_Light_POST.Extensions;
using Social_Light_POST.Services;
using Social_Light_POST.Services.IService;
using Social_Light_POST.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddScoped<ICommentService, CommentsService>();

builder.Services.AddHttpClient("Comments", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentsApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
    

//custom builders

builder.AddSwaggerGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.MapControllers();

app.Run();
