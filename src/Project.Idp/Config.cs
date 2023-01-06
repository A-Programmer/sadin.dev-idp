using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Project.Idp;

public static class Config
{
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "blazorapp",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = 
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = 
                {
                    "Website.api"
                }
            },
            // This one is using for sending machine to machine requests and this is setting in builder.Services.AddSingleton(new ClientCredentialsTokenRequest() on Program.cs class in ClientApp project
            new Client
            {
                ClientId = "websiteadmin",
                ClientName = "Website Admin",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowRememberConsent = false,
                RedirectUris = new List<string>
                {
                    "http://localhost:5001/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    "http://localhost:5001/signout-callback-oidc"
                },
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Website.api"
                }
            },
            // This client is using in .AddOpenIdConnect section of Client Program.cs class
            new Client
            {
                ClientId = "websiteadmin-login",
                ClientName = "Website Admin Login",
                AllowedGrantTypes = GrantTypes.Code,
                AllowRememberConsent = false,
                RedirectUris = new List<string>
                {
                    "http://localhost:5001/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    "http://localhost:5001/signout-callback-oidc"

                },
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Website.api"
                }
            }
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("Website.api", "Website API")
        };
    
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {

        };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "64db5bee-5490-490c-b7a2-49b7b1f9dd76",
                Username = "admin",
                Password = "Kamran123!",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, "Kamran"),
                    new Claim(JwtClaimTypes.FamilyName, "Sadin"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            },
            new TestUser
            {
                SubjectId = "00ee578b-b218-4472-95dc-3b5a73278ee0",
                Username = "user",
                Password = "Kamran123!",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, "Mohsen"),
                    new Claim(JwtClaimTypes.FamilyName, "Safari"),
                    new Claim(JwtClaimTypes.Role, "user")
                }
            }
        };
}
