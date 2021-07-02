using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Services.Token
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model, CancellationToken cancellationToken);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model, CancellationToken cancellationToken);
    }
}
