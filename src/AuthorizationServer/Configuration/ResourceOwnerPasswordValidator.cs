using IdentityModel;
using IdentityServer4.Core.Validation;
using AuthorizationServer.Repository;
using AuthorizationServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public Task<CustomGrantValidationResult> ValidateAsync(string userName, string password, ValidatedTokenRequest request)
        {
            bool validCredential = _userRepository.ValidateCredentials(userName,password);
            if (validCredential)
            {
                var user = _userRepository.FindByName(userName);
                //CustomGrantValidationResult 传入的subject,和Claims参数，将会在GetProfileDataAsync中被获取到，
                //并用于生成access_token
                var result = new CustomGrantValidationResult(user.Id, "password");
                return Task.FromResult(result);
            }
            else
            {
                var result = new CustomGrantValidationResult("Username Or Password Incorrect");
                return Task.FromResult(result);
            }
        }
    }
}
