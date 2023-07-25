using Blogging_Platform.Models.DTO;

namespace Blogging_Platform.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);

        Task<Status> RegistrationAsync(RegistrationModel model);

        Task LogoutAsync();
    }
}
