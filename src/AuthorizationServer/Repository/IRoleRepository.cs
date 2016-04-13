using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Repository
{
    public interface IRoleRepository
    {
        IdentityRole FindById(string id);
    }
}
