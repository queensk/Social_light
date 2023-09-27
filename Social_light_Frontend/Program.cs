using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Social_light_Frontend;
using Social_light_Frontend.Models.Config;
using Social_light_Frontend.Services.Auth;
using Social_light_Frontend.Services.AuthProvider;
using Social_light_Frontend.Services.AzureFileUpload;
using Social_light_Frontend.Services.Comment;
using Social_light_Frontend.Services.Post;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.Configure<AzureStorageConfig>(options => {
    options.AccountName =builder.Configuration.GetValue<string>("AzureStorageConfig:AccountName");
    options.AccountKey = builder.Configuration.GetValue<string>("AzureStorageConfig:AccountKey");
});

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvideService>();
builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

await builder.Build().RunAsync();
