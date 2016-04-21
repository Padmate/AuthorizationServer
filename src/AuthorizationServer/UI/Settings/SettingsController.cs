using AuthorizationServer.Models;
using AuthorizationServer.Utility;
using IdentityServer4.Core.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFramework7.Entities;
using Microsoft.Data.Entity;
using AuthorizationServer.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthorizationServer.UI.Settings
{
    public class SettingsController:Controller
    {
        private ScopeConfigurationContext _scopeContext;
        private ClientConfigurationContext _clientContext;
        private OperationalContext _operationalContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SettingsController(ClientConfigurationContext clientContext,
            ScopeConfigurationContext scopeContext,
            OperationalContext operationalContext,
            UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager)
        {
            _clientContext = clientContext;
            _scopeContext = scopeContext;
            _operationalContext = operationalContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public ActionResult Index()
        {
            return View();
        }

        #region Clients
        public ActionResult ClientManage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateClient(ClientViewModel model)
        {
            try
            {
                #region Add Client
                Client client = new Client();
                client.ClientId = model.ClientId;
                client.ClientName = model.ClientName;
                client.ClientUri = model.ClientUri;
                client.Flow = (Flows)(System.Convert.ToInt32(model.Flow));
               
                if (!string.IsNullOrEmpty(model.ClientSecret))
                {
                    client.ClientSecrets = new List<Secret>()
                {
                    new Secret(model.ClientSecret.Sha256())
                };
                }
                if (!string.IsNullOrEmpty(model.RedirectUri))
                {
                    client.RedirectUris = model.RedirectUri.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                client.AllowedScopes = new List<string>
            {
                StandardScopes.OpenId.Name,
                StandardScopes.Profile.Name,
                StandardScopes.Email.Name,
                StandardScopes.Roles.Name,
                "dpcontrolapiscope"
            };

                AddClients(client);
                #endregion
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "新增失败："+e.Message);
            }
            

            return View("Index",model);
        }

        private void AddClients(Client ct)
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
                RequireConsent = ct.RequireConsent, //设置为true会有问题？
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
                    Expiration = secret.Expiration.HasValue ? (DateTime?)secret.Expiration.Value.DateTime : null,
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
            _clientContext.SaveChanges();
        }


        [HttpPost]
        public ActionResult EditClient(ClientViewModel model)
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult DeleteClient(string id)
        {
            var client = _clientContext.Clients.FirstOrDefault(c=>c.Id == new Guid(id));
            _clientContext.Remove(client);
            _clientContext.SaveChanges();
            
            return View("Index");

        }

        public IActionResult GetClientsPageData()
        {
            //get params
            HttpRequest rq = Request;
            StreamReader srRequest = new StreamReader(rq.Body);
            String strReqStream = srRequest.ReadToEnd();
            BaseModel baseModel = JsonHandler.DeserializeJsonToObject<BaseModel>(strReqStream);

            var allClients = _clientContext.Clients.ToList();
            var pageClients = _clientContext.Clients
                .Skip(baseModel.offset).Take(baseModel.limit).ToList();
            PageResult<Client<Guid>> pageResult = new PageResult<Client<Guid>>(allClients.Count, pageClients);
            return Json(pageResult);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetClientAllDetail(string id)
        {
            var client = await _clientContext.Clients
                .Include(c=>c.ClientSecrets)
                .Include(c=>c.AllowedScopes)
                .Include(c=>c.RedirectUris)
                .FirstOrDefaultAsync(c=>c.Id == new Guid(id));

            ClientModel result = new ClientModel();
            result.Id = client.Id.ToString() ;
            result.ClientId = client.ClientId;
            result.ClientName = client.ClientName;
            result.ClientUri = client.ClientUri;
            result.Flow = System.Convert.ToInt32(client.Flow);
            result.RequireConsent = client.RequireConsent;
            result.ClientSecret = client.ClientSecrets.Count > 0 ? client.ClientSecrets.First().Value : string.Empty ;
            result.AllowedScopes = client.AllowedScopes.Count >0?client.AllowedScopes.Select(c => c.Scope).ToList():null;
            result.RedirectUris = client.RedirectUris.Count>0 ?client.RedirectUris.Select(c=>c.Uri).ToList():null;
            //确定返回的类型，如果不确定则会导致ajax 502 Bad Gateway 错误
            return Json(result);
        }

        #endregion

        #region Scopes
        public ActionResult ScopeManage()
        {
            return View();
        }

        public ActionResult CreateScopes()
        {
            AddScopes();

            return View("Index");
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
                        Description = claim.Description,
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
                        Expiration = secret.Expiration.HasValue ? (DateTime?)secret.Expiration.Value.DateTime : null,
                        Type = secret.Type,
                        Value = secret.Value

                    };
                    scope.ScopeSecrets.Add(scopeSecret);

                }

                _scopeContext.Scopes.Add(scope);


            }
            _scopeContext.SaveChanges();
        }

        public ActionResult DeleteScopes()
        {
            _scopeContext.RemoveRange(_scopeContext.Scopes);
            _scopeContext.SaveChanges();
            

            return View("Index");
        }

        public IActionResult GetScopesPageData()
        {
            //get params
            HttpRequest rq = Request;
            StreamReader srRequest = new StreamReader(rq.Body);
            String strReqStream = srRequest.ReadToEnd();
            BaseModel baseModel = JsonHandler.DeserializeJsonToObject<BaseModel>(strReqStream);

            var allScopes = _scopeContext.Scopes.ToList();
            var pageScopes = _scopeContext.Scopes
                .Skip(baseModel.offset).Take(baseModel.limit).ToList();
            PageResult<Scope<Guid>> pageResult = new PageResult<Scope<Guid>>(allScopes.Count, pageScopes);
            return Json(pageResult);
        }
        #endregion

        #region Role

        [HttpGet]
        public ActionResult RoleManage()
        {
            return View();
        }

        /// <summary>
        /// Get Role Paging Data
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRolePageData()
        {
            //get params
            HttpRequest rq = Request;
            StreamReader srRequest = new StreamReader(rq.Body);
            String strReqStream = srRequest.ReadToEnd();
            BaseModel baseModel = JsonHandler.DeserializeJsonToObject<BaseModel>(strReqStream);

            var allRoles = _roleManager.Roles.ToList();
            var pageRoles = _roleManager.Roles
                .Skip(baseModel.offset).Take(baseModel.limit).ToList();
            PageResult<IdentityRole> pageResult = new PageResult<IdentityRole>(allRoles.Count, pageRoles);
            return Json(pageResult);
            
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> CreateRole(string RoleName)
        {

            Message message = new Message();
            message.Success = true;
            if (string.IsNullOrEmpty(RoleName))
            {
                message.Success = false;
                message.Content = "Role Name is required";
                return Json(message);
            }

            //新增角色
            IdentityRole adminRole = new IdentityRole { Name = RoleName, NormalizedName = RoleName.ToUpper() };
            var result = await _roleManager.CreateAsync(adminRole);
            if (!result.Succeeded)
            {
                message.Success = false;
                message.Content = result.Errors.First().Description;
                return Json(message);

            }
            return Json(message);

        }

        public async Task<IActionResult> BachDeleteByRoleId(string RoleIds)
        {
            Message message = new Message();
            message.Success = true;

            List<string> roleIds = JsonHandler.UnJson<List<string>>(RoleIds);
            foreach (string roleid in roleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleid);
                var result = await _roleManager.DeleteAsync(role);

            }

            return Json(message);
        }

        #endregion

        #region User
        public ActionResult UserManage()
        {
            return View();
        }
        #endregion
    }

}
