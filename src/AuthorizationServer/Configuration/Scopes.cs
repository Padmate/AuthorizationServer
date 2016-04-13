using IdentityServer4.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>()
            {
                
                StandardScopes.OpenId,
                StandardScopes.ProfileAlwaysInclude,
                StandardScopes.EmailAlwaysInclude,
                StandardScopes.OfflineAccess,
                StandardScopes.RolesAlwaysInclude,
                new Scope
                {
                    //IncludeAllClaimsForUser = true ,
                    Name = "dpcontrolapiscope",
                    DisplayName = "DpControl ApiScope",
                    Description = "DpControl Restful API",
                    Type = ScopeType.Resource,
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("dpcontrolapiscopesecret".Sha256())
                    },
                    Claims = new List<ScopeClaim>
                    {
                        //此处声明的Claim将会包含在access_token中
                        new ScopeClaim("role"),
                        new ScopeClaim("name"),
                        new ScopeClaim("profile"),
                        new ScopeClaim("email")
                    }
                },

            };
            return scopes;
        }
    }
}
