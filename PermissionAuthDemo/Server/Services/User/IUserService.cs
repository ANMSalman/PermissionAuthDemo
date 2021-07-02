using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Services.User
{
    public interface IUserService
    {
        Task<Result<List<UserResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<IResult> RegisterAsync(RegisterRequest request, string origin, CancellationToken cancellationToken);
        Task<IResult<UserResponse>> GetAsync(string userId, CancellationToken cancellationToken);
        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken);
        Task<IResult<UserRolesResponse>> GetRolesAsync(string userId, CancellationToken cancellationToken);
        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request, CancellationToken cancellationToken);
        Task<IResult<string>> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken);
        Task<int> GetCountAsync(CancellationToken cancellationToken);
    }
}