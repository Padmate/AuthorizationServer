using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.UI.Settings
{
    public class ClientViewModel
    {
        public string Id { get; set; }
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        
        public string ClientSecret { get; set; }

        [Required]
        public string Flow { get; set; }

        public string RedirectUri { get; set; }
        
        public List<string> RedirectUris {
            get
            {
                var result = new List<string>();
                if (!string.IsNullOrEmpty(RedirectUri))
                {
                    RedirectUri = RedirectUri.Replace("\n\r","");
                   result = RedirectUri.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                return result;
            }  
            set { }
        }

        public string AllowScope { get; set; }
        public List<string> AllowScopes
        {
            get
            {
                var result = new List<string>();
                if (!string.IsNullOrEmpty(AllowScope))
                {
                    AllowScope = RedirectUri.Replace("\n\r", "");
                    result = AllowScope.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                return result;
            }
            set { }
        }

        public bool RequireConsents { get; set; }
    }
}
