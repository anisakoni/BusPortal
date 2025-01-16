using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services.Scoped
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password, string name)
        {
            var existingUser = await _userRepository.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already registered." });
            }

            var user = new IdentityUser
            {
                Email = email,
                UserName = name
            };

            return await _userRepository.CreateUserAsync(user, password);
        }

        public async Task<SignInResult> LoginUserAsync(string email, string password, bool rememberMe)
        {
            return await _userRepository.LoginAsync(email, password, rememberMe);
        }

        public async Task LogoutUserAsync()
        {
            await _userRepository.LogoutAsync();
        }
    }
}
