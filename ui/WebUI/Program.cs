using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.HttpClientUtilities;
using WebUI.Session;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var apiUrl = builder.Configuration.GetValue<string>("ApiUrl")
             ?? throw new NullReferenceException("Api url not found or declared");

builder.Services
    .AddHttpClient<ApiHttpClient>(o =>
    {
        o.BaseAddress = new Uri(apiUrl);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler()
        {
            UseCookies = false,
        };
        return handler;
    });

builder.Services.AddSingleton<SessionHelper>();
builder.Services.AddSingleton<JwtApplication>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

WebUI.DependencyResolver.DependencyResolverService.Register(builder.Services);

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();