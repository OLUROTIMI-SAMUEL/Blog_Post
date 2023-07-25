using Blogging_Platform.Models.Domain;
using Blogging_Platform.Models.DTO;
using Blogging_Platform.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blogging_Platform.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> rolemanager;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolemanager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.rolemanager = rolemanager;
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
            // throw new NotImplementedException();
            var status = new Status();
            var user = await userManager.FindByIdAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }
            //we will match password
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User locked out";
                return status;

            }
            else
            {
                status.StatusCode = 0;
                status.Message = " Error on logging in ";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            // throw new NotImplementedException();

            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            // throw new NotImplementedException();
            var status = new Status();
            var userExists = await userManager.FindByEmailAsync(model.Username);
            if (userExists == null)
            {
                status.StatusCode = 0;
                status.Message = "User already exist";
                return status;
            }

            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            //our role management 
            if (!await rolemanager.RoleExistsAsync(model.Role))
                await rolemanager.CreateAsync(new IdentityRole(model.Role));

            if (await rolemanager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }
            status.StatusCode = 1;
            status.Message = "User has registered sucessfully";
            return status;
        }
    }
}
