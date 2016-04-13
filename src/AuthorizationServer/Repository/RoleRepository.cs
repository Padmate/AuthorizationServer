using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using AuthorizationServer.Models;

namespace AuthorizationServer.Repository
{
    public class RoleRepository : IRoleRepository
    {
        IdSvrDbContext _context;

        #region construct
        public RoleRepository()
        {

        }
        public RoleRepository(IdSvrDbContext context)
        {
            _context = context;
        }
        #endregion

        public IdentityRole FindById(string id)
        {
            var role = _context.Roles.Where(r => r.Id == id).FirstOrDefault();
            return role;
        }
    }
}
