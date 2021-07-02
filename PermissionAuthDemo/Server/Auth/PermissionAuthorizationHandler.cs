using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PermissionAuthDemo.Server.Data.Entities;
using PermissionAuthDemo.Shared.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Auth
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public PermissionAuthorizationHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            if (context.User == null)
            {
                return;
            }

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            var user = await _userManager.GetUserAsync(context.User);
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var permissions = roleClaims
                    .Where(x => x.Type == ApplicationClaimTypes.Permission
                                && x.Value == requirement.Permission
                                && x.Issuer == "LOCAL AUTHORITY")
                    .Select(x => x.Value);


                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}
