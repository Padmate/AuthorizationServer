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

namespace AuthorizationServer.UI.Settings
{
    public class SettingsController:Controller
    {
        private ScopeConfigurationContext _scopeContext;
        private ClientConfigurationContext _clientContext;
        private OperationalContext _operationalContext;

        public SettingsController(ClientConfigurationContext clientContext,
            ScopeConfigurationContext scopeContext, OperationalContext operationalContext)
        {
            _clientContext = clientContext;
            _scopeContext = scopeContext;
            _operationalContext = operationalContext;
        }


        public ActionResult Index()
        {
            ViewData["ListType"] = "";
            return View();
        }

        #region Clients
        [HttpPost]
        public ActionResult CreateClient(ClientViewModel model)
        {
            ViewData["ListType"] = "client";
            return View("Index");
        }

        public ActionResult EditClient(ClientViewModel model)
        {
            ViewData["ListType"] = "client";
            return View("Index");
        }

        public ActionResult DeleteClient(string id)
        {

            ViewData["ListType"] = "client";
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
            result.Flow = client.Flow.ToString();
            result.RequireConsent = client.RequireConsent;
            result.ClientSecret = client.ClientSecrets.Count > 0 ? client.ClientSecrets.First().Value : string.Empty ;
            result.AllowedScopes = client.AllowedScopes.Count >0?client.AllowedScopes.Select(c => c.Scope).ToList():null;
            result.RedirectUris = client.RedirectUris.Count>0 ?client.RedirectUris.Select(c=>c.Uri).ToList():null;
            //确定返回的类型，如果不确定则会导致ajax 502 Bad Gateway 错误
            return Json(result);
        }

        #endregion

    }
    
}
