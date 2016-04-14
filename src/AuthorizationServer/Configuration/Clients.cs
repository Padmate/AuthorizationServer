using IdentityServer4.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            var clients = new List<Client>()
            {
                ///////////////////////////////////////////
                //AuthorizationCode Flow
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "padmate_AuthorizationCode",
                    ClientName = "Authorization Code",
                    ClientUri = "http://identityserver.io",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("padmate_authorizationcode_secret".Sha256())
                    },
                    Flow = Flows.AuthorizationCode,
                    //Specifies allowed URIs to return tokens or authorization codes to
                    RedirectUris = new List<string>
                    {
                         "http://localhost:27898/Home/CallBack" //Client URI
                    },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.Email.Name,
                        StandardScopes.Roles.Name,
                        "dpcontrolapiscope"
                    }

                },

                ///////////////////////////////////////////
                // Implicit Flow
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "padmate_mvcimplicit",
                    ClientName = "MVC Implicit",
                    ClientUri = "http://identityserver.io",

                    Flow = Flows.Implicit,
                    //Specifies allowed URIs to return tokens or authorization codes to
                    RedirectUris = new List<string>
                    {
                         "http://localhost:15142/Home/CallBack" //Client URI
                         //"http://mvcclient.chinacloudsites.cn/Home/CallBack"
                    },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.Email.Name,
                        StandardScopes.Roles.Name,
                        "dpcontrolapiscope"
                    }

                },
                ///////////////////////////////////////////
                // Console Resource Owner Flow Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "padmate_resourceowner",
                    ClientName="Resource Owner",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("padmate_resourceowner_secret".Sha256())
                    },

                    Flow = Flows.ResourceOwner,
                    //AccessTokenLifteTime must be setted at lease 5 min
                    AccessTokenLifetime = 3600,  //1 hour   
                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Email.Name,
                        StandardScopes.OfflineAccess.Name,
                        StandardScopes.Roles.Name,
                        StandardScopes.Profile.Name,
                        "dpcontrolapiscope"
                    }
                }

            };
            return clients;

        }
    }
}
