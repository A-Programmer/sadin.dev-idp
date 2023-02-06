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

// Uncomment this section to use CERT file in production
// if (builder.Environment.IsDevelopment())
// {
    builder.Services.AddIdentityServer()
        .AddInMemoryClients(Config.Clients)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddTestUsers(Config.TestUsers)
        .AddDeveloperSigningCredential();
// }
// else
// {
//     // TODO : this part does not work fine, after deploying I am getting error that can not fine cert file path but
//     // I think the issue comes from the cert file, I should generate new one and provide the new one
//     X509Certificate2 cert = null;
//     cert = new X509Certificate2(Path.Combine(builder.Environment.ContentRootPath, "wwwroot/certs", "aspnetapp.pfx"), "123456");
//     builder.Services.AddIdentityServer()
//         .AddInMemoryClients(Config.Clients)
//         .AddInMemoryApiScopes(Config.ApiScopes)
//         .AddInMemoryIdentityResources(Config.IdentityResources)
//         .AddTestUsers(Config.TestUsers)
//         .AddSigningCredential(cert);
// }
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
