using AuthorizationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Repository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByNameAsync(string username);
        ApplicationUser FindByName(string username);

        Task<ApplicationUser> FindByIdAsync(string id);
        ApplicationUser FindById(string id);
        bool ValidateCredentials(string username, string password);
    }
}
