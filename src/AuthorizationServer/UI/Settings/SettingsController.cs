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
        public ActionResult Client(ClientViewModel model)
        {
            ViewData["ListType"] = "client";
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index",model);
        }

        public IActionResult GetClientsPageData()
        {
            //get params
            HttpRequest rq = Request;
            StreamReader srRequest = new StreamReader(rq.Body);
            String strReqStream = srRequest.ReadToEnd();
            BaseModel baseModel = JsonHandler.DeserializeJsonToObject<BaseModel>(strReqStream);

            var allClients = _clientContext.Clients.ToList();
            var pageClients = _clientContext.Clients.Skip(baseModel.offset).Take(baseModel.limit).ToList();
            PageResult<Client<Guid>> pageResult = new PageResult<Client<Guid>>(allClients.Count, pageClients);
            return Json(pageResult);
        }

        #endregion

    }
}
