using AuthorizationServer.Configuration;
using AuthorizationServer.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFramework7.Entities;

namespace AuthorizationServer.UI.Home
{
    public class HomeController : Controller
    {
        private ScopeConfigurationContext _scopeContext;
        private ClientConfigurationContext _clientContext;

        public HomeController(ClientConfigurationContext clientContext, ScopeConfigurationContext scopeContext)
        {
            _clientContext = clientContext;
            _scopeContext = scopeContext;
        }
        

        [Route("/")]
        public IActionResult Index()
        {
            //AddScopes();
            //var scopes = _scopeContext.Scopes.ToList();

            //AddClients();
            //var clients = _clientContext.Clients.ToList();
            return View();
        }

        public IActionResult AddClientsAndScopes()
        {
            //AddScopes();
            //var scopes = _scopeContext.Scopes.ToList();

            AddClients();
            var clients = _clientContext.Clients.ToList();

            return Json("Success");
        }

        private void AddClients()
        {

            foreach (var ct in Clients.Get())
            {
                var client = new Client<Guid>()
                {
                    AbsoluteRefreshTokenLifetime = ct.AbsoluteRefreshTokenLifetime,
                    AccessTokenLifetime = ct.AccessTokenLifetime,
                    AccessTokenType = ct.AccessTokenType,
                    AllowAccessToAllGrantTypes = ct.AllowAccessToAllCustomGrantTypes,
                    AllowAccessToAllScopes = ct.AllowAccessToAllScopes,
                    AllowClientCredentialsOnly = ct.AllowClientCredentialsOnly,
                    AllowPromptNone = ct.AllowPromptNone,
                    AllowRememberConsent = ct.AllowRememberConsent,
                    AlwaysSendClientClaims = ct.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = ct.AuthorizationCodeLifetime,
                    ClientId = ct.ClientId,
                    ClientName = ct.ClientName,
                    ClientUri = ct.ClientUri,
                    EnableLocalLogin = ct.EnableLocalLogin,
                    Enabled = ct.Enabled,
                    Flow = ct.Flow,
                    IdentityTokenLifetime = ct.IdentityTokenLifetime,
                    IncludeJwtId = ct.IncludeJwtId,
                    LogoUri = ct.LogoUri,
                    LogoutSessionRequired = ct.LogoutSessionRequired,
                    LogoutUri = ct.LogoutUri,
                    PrefixClientClaims = ct.PrefixClientClaims,
                    RefreshTokenExpiration = ct.RefreshTokenExpiration,
                    RefreshTokenUsage = ct.RefreshTokenUsage,
                    RequireConsent = false, //设置为true会有问题？
                    SlidingRefreshTokenLifetime = ct.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenOnRefresh = ct.UpdateAccessTokenClaimsOnRefresh

                };
                client.ClientSecrets = new List<ClientSecret<Guid>>();
                client.AllowedScopes = new List<ClientScope<Guid>>();
                client.RedirectUris = new List<ClientRedirectUri<Guid>>();
                client.IdentityProviderRestrictions = new List<ClientProviderRestriction<Guid>>();
                client.PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri<Guid>>();
                client.AllowedCustomGrantTypes = new List<ClientCustomGrantType<Guid>>();
                client.Claims = new List<ClientClaim<Guid>>();
                client.AllowedCorsOrigins = new List<ClientCorsOrigin<Guid>>();
                
                //Add Secrets
                foreach (var secret in ct.ClientSecrets)
                {
                    var clientSecret = new ClientSecret<Guid>()
                    {
                        Description = secret.Description,
                        Expiration = secret.Expiration.HasValue?(DateTime?)secret.Expiration.Value.DateTime:null,
                        Type = secret.Type,
                        Value = secret.Value
                    };
                    client.ClientSecrets.Add(clientSecret);
                }
                //Add Scopes
                foreach (var scope in ct.AllowedScopes)
                {
                    var allowedScope = new ClientScope<Guid>()
                    {
                        Scope = scope
                    };
                    client.AllowedScopes.Add(allowedScope);
                }

                //Add RedirectUris
                foreach (var redirectUri in ct.RedirectUris)
                {
                    var clientRedirectUri = new ClientRedirectUri<Guid>()
                    {
                        Uri = redirectUri
                    };
                    client.RedirectUris.Add(clientRedirectUri);
                }
                //Add ProviderRestrictions
                foreach (var provider in ct.IdentityProviderRestrictions)
                {
                    var clientProviderRestriction = new ClientProviderRestriction<Guid>()
                    {
                        Provider = provider
                    };
                    client.IdentityProviderRestrictions.Add(clientProviderRestriction);
                }
                //Add ClientPostLogoutRedirectUri
                foreach (var postLogoutRedirectUri in ct.PostLogoutRedirectUris)
                {
                    var clientPostLogoutRedirectUri = new ClientPostLogoutRedirectUri<Guid>()
                    {
                        Uri = postLogoutRedirectUri
                    };
                    client.PostLogoutRedirectUris.Add(clientPostLogoutRedirectUri);
                }
                //Add ClientCustomGrantTypes
                foreach (var customGrantType in ct.AllowedCustomGrantTypes)
                {
                    var clientCustomerGrantType = new ClientCustomGrantType<Guid>()
                    {
                        GrantType = customGrantType
                    };
                    client.AllowedCustomGrantTypes.Add(clientCustomerGrantType);
                }
                //Add ClientClaims
                foreach (var claim in ct.Claims)
                {
                    var clientClaim = new ClientClaim<Guid>()
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    };
                    client.Claims.Add(clientClaim);
                }
                //Add ClientCorsOrigins
                foreach (var corsOrigin in ct.AllowedCorsOrigins)
                {
                    var clientCorsOrigin = new ClientCorsOrigin<Guid>()
                    {
                       Origin = corsOrigin
                    };
                    client.AllowedCorsOrigins.Add(clientCorsOrigin);
                }

                _clientContext.Clients.Add(client);
            }
            _clientContext.SaveChanges();
        }

        private void AddScopes()
        {
            foreach (var sc in Scopes.Get())
            {
                var scope = new Scope<Guid>()
                {
                    
                    ClaimsRule = sc.ClaimsRule,
                    Description = sc.Description,
                    DisplayName = sc.DisplayName,
                    Emphasize = sc.Emphasize,
                    Enabled = sc.Enabled,
                    IncludeAllClaimsForUser = sc.IncludeAllClaimsForUser,
                    Name = sc.Name,
                    Required = sc.Required,
                    ShowInDiscoveryDocument = sc.ShowInDiscoveryDocument,
                    Type = (int)sc.Type,
                    AllowUnrestrictedIntrospection = sc.AllowUnrestrictedIntrospection,

                };
                scope.ScopeClaims = new List<ScopeClaim<Guid>>();
                scope.ScopeSecrets = new List<ScopeSecret<Guid>>();

                //Add ScopeClaims
                foreach (var claim in sc.Claims)
                {
                    var scopeClaim = new ScopeClaim<Guid>()
                    {
                        
                        AlwaysIncludeInIdToken = claim.AlwaysIncludeInIdToken,
                        Description = claim.Description ,
                        Name = claim.Name
                        
                    };
                    scope.ScopeClaims.Add(scopeClaim);

                }

                //AddScopeSecrets
                foreach (var secret in sc.ScopeSecrets)
                {
                    var scopeSecret = new ScopeSecret<Guid>()
                    {
                        Description = secret.Description,
                        Expiration = secret.Expiration.HasValue? (DateTime?)secret.Expiration.Value.DateTime:null,
                        Type = secret.Type,
                        Value = secret.Value

                    };
                    scope.ScopeSecrets.Add(scopeSecret);

                }

                _scopeContext.Scopes.Add(scope);
               

            }
            _scopeContext.SaveChanges();
        }
    }
}
