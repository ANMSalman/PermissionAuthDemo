using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Services.Role
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}
