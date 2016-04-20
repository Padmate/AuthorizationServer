using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.UI.Settings
{
    public class ClientModel
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public Int32 Flow { get; set; }
        public bool RequireConsent { get; set; }

        public string ClientSecret { get; set; }
        public List<string> AllowedScopes { get; set; }
        public List<string> RedirectUris { get; set; }
    }
}
