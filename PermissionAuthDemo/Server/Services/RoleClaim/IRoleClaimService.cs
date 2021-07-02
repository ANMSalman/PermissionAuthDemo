using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Services.RoleClaim
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> GetCountAsync(CancellationToken cancellationToken);

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId, CancellationToken cancellationToken);

        Task<Result<string>> SaveAsync(RoleClaimRequest request, CancellationToken cancellationToken);

        Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
