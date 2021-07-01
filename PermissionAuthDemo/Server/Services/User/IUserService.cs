using System.Collections.Generic;
using System.Threading.Tasks;
using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;

namespace PermissionAuthDemo.Server.Services.User
{
    public interface IUserService
    {
        Task<Result<List<UserResponse>>> GetAllAsync();
        Task<IResult> RegisterAsync(RegisterRequest request, string origin);
        Task<IResult<UserResponse>> GetAsync(string userId);
        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);
        Task<IResult<UserRolesResponse>> GetRolesAsync(string userId);
        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);
        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);
        Task<int> GetCountAsync();
    }
}