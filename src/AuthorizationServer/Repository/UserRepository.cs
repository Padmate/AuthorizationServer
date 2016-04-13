using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Models;
using Microsoft.Data.Entity;
using IdentityServer4.Core.Models;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using AuthorizationServer.Utility;
using Microsoft.AspNet.Identity;

namespace AuthorizationServer.Repository
{
    public class UserRepository : IUserRepository
    {
        UserDbContext _context;

        #region construct
        public UserRepository()
        {

        }
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public ApplicationUser FindById(string id)
        {
            var user = _context.Users.Include(u => u.Roles)
                .Where(u => u.Id == id)
                .FirstOrDefault();
            return user;
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            var user = await _context.Users.Include(u => u.Roles)
               .Where(u => u.Id == id)
               .FirstOrDefaultAsync();
            return user;
        }
        #endregion

        public ApplicationUser FindByName(string userName)
        {
            var user = _context.Users.Include(u=>u.Roles)
                .Where(u => u.UserName == userName)
                .FirstOrDefault();
            return user;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = await _context.Users.Include(u=>u.Roles)
                .Where(u => u.UserName == userName)
                .FirstOrDefaultAsync();
            return user;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = this.FindByName(username);
            if (user != null)
            {
                Security s = new Security();
                var verifyPassword =  s.VerifyHashedPassword(null, user.PasswordHash, password);
                if (PasswordVerificationResult.Success == verifyPassword)
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
