using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity;
using AuthorizationServer.Models;

namespace AuthorizationServer.Utility
{
    public class Security : PasswordHasher<ApplicationUser>
    {
    }
}
