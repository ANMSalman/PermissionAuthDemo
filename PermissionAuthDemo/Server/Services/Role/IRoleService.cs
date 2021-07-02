using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Services.Role
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> GetCountAsync(CancellationToken cancellationToken);

        Task<Result<RoleResponse>> GetByIdAsync(string id, CancellationToken cancellationToken);

        Task<Result<string>> SaveAsync(RoleRequest request, CancellationToken cancellationToken);

        Task<Result<string>> DeleteAsync(string id, CancellationToken cancellationToken);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId, CancellationToken cancellationToken);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request, CancellationToken cancellationToken);
    }
}
