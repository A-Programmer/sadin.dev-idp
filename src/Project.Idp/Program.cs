using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Project.Idp;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration;

if (builder.Environment.IsProduction())
{
    Configuration = Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
}
else
{
    Configuration = Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Development.json")
        .Build();
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddIdentityServer()
        .AddInMemoryClients(Config.Clients)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddTestUsers(Config.TestUsers)
        .AddDeveloperSigningCredential();
}
else
{
    
    X509Certificate2 cert = null;
    cert = new X509Certificate2(Path.Combine("wwwroot\\certs", "aspnetapp.pfx"), "123456");
    builder.Services.AddIdentityServer()
        .AddInMemoryClients(Config.Clients)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddTestUsers(Config.TestUsers)
        .AddSigningCredential(cert);
}
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); 
app.UseRouting();
app.UseAuthorization();
app.UseIdentityServer();

app.UseEndpoints(endpoint =>
{
    endpoint.MapDefaultControllerRoute();
});

app.Run();
