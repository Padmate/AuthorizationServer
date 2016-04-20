using IdentityServer4.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class AllowAllCorsPolicy : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            //AllowAllOrigin
            return Task.FromResult(true);
        }
    }
}
