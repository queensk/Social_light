using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Social_light_Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppAuthentication();


if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
{
    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseCors("policy1");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
