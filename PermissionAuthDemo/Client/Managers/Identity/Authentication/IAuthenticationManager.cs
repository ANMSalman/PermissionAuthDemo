using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client.Managers.Identity.Authentication
{
    public interface IAuthenticationManager : IManager
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();

        Task<ClaimsPrincipal> CurrentUser();
    }
}