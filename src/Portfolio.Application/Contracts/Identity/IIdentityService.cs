using Microsoft.AspNetCore.Identity;
using Portfolio.Application.Models.Identity;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<SignInResult> AuthenticateAsync(AuthenticationModel request);
        Task<IdentityResult> ConfirmEmail(string email, string token);
        Task<string> GeneratePasswordForgottenMailToken(string email);
        Task<string> GenerateRegistrationEncodedToken(string id);
        Task<ApplicationUser> GetUserOrNullAsync(string email);
        Task<IdentityResult> RegisterAdmin(RegistrationModel model);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
        Task SignOutAsync();
        Task<bool> UserEmailExist(string email);
        Task<bool> UsernameExist(string name);
        Task<bool> UserTableIsEmpty();
    }
}
