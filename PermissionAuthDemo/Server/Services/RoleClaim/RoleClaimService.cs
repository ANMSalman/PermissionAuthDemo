using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PermissionAuthDemo.Server.Data;
using PermissionAuthDemo.Server.Data.Entities;
using PermissionAuthDemo.Server.Services.CurrentUser;
using PermissionAuthDemo.Shared.Requests.Identity;
using PermissionAuthDemo.Shared.Responses.Identity;
using PermissionAuthDemo.Shared.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Services.RoleClaim
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly AppDbContext _db;

        public RoleClaimService(
            IMapper mapper,
            ICurrentUserService currentUserService,
            AppDbContext db)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _db = db;
        }

        public async Task<Result<List<RoleClaimResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var roleClaims = await _db.RoleClaims.ToListAsync(cancellationToken: cancellationToken);
            var roleClaimsResponse = _mapper.Map<List<RoleClaimResponse>>(roleClaims);
            return await Result<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken)
        {
            var count = await _db.RoleClaims.CountAsync(cancellationToken: cancellationToken);
            return count;
        }

        public async Task<Result<RoleClaimResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var roleClaim = await _db.RoleClaims
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            var roleClaimResponse = _mapper.Map<RoleClaimResponse>(roleClaim);
            return await Result<RoleClaimResponse>.SuccessAsync(roleClaimResponse);
        }

        public async Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var roleClaims = await _db.RoleClaims
                .Include(x => x.Role)
                .Where(x => x.RoleId == roleId)
                .ToListAsync(cancellationToken: cancellationToken);
            var roleClaimsResponse = _mapper.Map<List<RoleClaimResponse>>(roleClaims);
            return await Result<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
        }

        public async Task<Result<string>> SaveAsync(RoleClaimRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RoleId))
            {
                return await Result<string>.FailAsync("Role is required.");
            }

            if (request.Id == 0)
            {
                var existingRoleClaim =
                    await _db.RoleClaims
                        .SingleOrDefaultAsync(x =>
                            x.RoleId == request.RoleId && x.ClaimType == request.Type && x.ClaimValue == request.Value, cancellationToken: cancellationToken);
                if (existingRoleClaim != null)
                {
                    return await Result<string>.FailAsync("Similar Role Claim already exists.");
                }
                var roleClaim = _mapper.Map<AppRoleClaim>(request);
                await _db.RoleClaims.AddAsync(roleClaim, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return await Result<string>.SuccessAsync($"Role Claim {request.Value} created.");
            }
            else
            {
                var existingRoleClaim =
                    await _db.RoleClaims
                        .Include(x => x.Role)
                        .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (existingRoleClaim == null)
                {
                    return await Result<string>.SuccessAsync("Role Claim does not exist.");
                }
                else
                {
                    existingRoleClaim.ClaimType = request.Type;
                    existingRoleClaim.ClaimValue = request.Value;
                    existingRoleClaim.Group = request.Group;
                    existingRoleClaim.Description = request.Description;
                    existingRoleClaim.RoleId = request.RoleId;
                    _db.RoleClaims.Update(existingRoleClaim);
                    await _db.SaveChangesAsync(cancellationToken);
                    return await Result<string>.SuccessAsync(
                        $"Role Claim {request.Value} for Role {existingRoleClaim.Role.Name} updated.");
                }
            }
        }

        public async Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingRoleClaim = await _db.RoleClaims
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (existingRoleClaim != null)
            {
                _db.RoleClaims.Remove(existingRoleClaim);
                await _db.SaveChangesAsync(cancellationToken);
                return await Result<string>.SuccessAsync(
                    $"Role Claim {existingRoleClaim.ClaimValue} for {existingRoleClaim.Role.Name} Role deleted.");
            }
            else
            {
                return await Result<string>.FailAsync("Role Claim does not exist.");
            }
        }
    }
}
