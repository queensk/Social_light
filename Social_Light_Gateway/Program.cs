using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Social_Light_Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// auth extension for checking the token
builder.AddAppAuthentication();
// configuration
// reloadOnChange: true reloads incase we change anything in the ocelot.json file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// Cors
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));

app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.UseCors("policy1");

app.Run();
