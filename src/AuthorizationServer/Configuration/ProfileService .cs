using IdentityModel;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using AuthorizationServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Configuration
{
    public class ProfileService : IProfileService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public ProfileService(IUserRepository userRepository,IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 该方法会被调用多次，没调用一次生成一种类型的token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //Get Subject value (which equal to UserId)
            string subject = context.Subject.Claims.ToList().Find(s => s.Type == "sub").Value;
            try
            {
                // Get Claims From Database, And Use Subject To Find The Related Claims,
                //As A Subject Is An Unique Identity Of User
                #region Construct Claims
                var user = _userRepository.FindById(subject);
                var claims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.Email,user.Email ==null ?"":user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed.ToString(), ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.BirthDate,DateTime.Now.ToString())

                };
                foreach (var userRole in user.Roles)
                {
                    var role = _roleRepository.FindById(userRole.RoleId);
                    claims.Add(new Claim(JwtClaimTypes.Role, role.Name));
                }
                #endregion
                context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
                
            }
            catch
            {
                //do nothing
            }
            return Task.FromResult(0);
            
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //var user = _userRepository.FindByName(context.Subject.Identity.Name);
            //return Task.FromResult(user != null);
            return Task.FromResult(0);
        }
    }
}
