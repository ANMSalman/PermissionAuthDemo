using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionAuthDemo.Server.Services.User;
using PermissionAuthDemo.Shared.Constants;
using PermissionAuthDemo.Shared.Requests.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Controllers.Identity
{
    [Authorize]
    [Route("api/identity/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToke)
        {
            var users = await _userService.GetAllAsync(cancellationToke);
            return Ok(users);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetAsync(id, cancellationToken);
            return Ok(user);
        }

        /// <summary>
        /// Get User Roles By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetRolesAsync(string id, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetRolesAsync(id, cancellationToken);
            return Ok(userRoles);
        }

        /// <summary>
        /// Update Roles for User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.Edit)]
        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRolesAsync(UpdateUserRolesRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _userService.UpdateRolesAsync(request, cancellationToken));
        }

        /// <summary>
        /// Register a User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _userService.RegisterAsync(request, origin, cancellationToken));
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns>Status 200 OK</returns>
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code, CancellationToken cancellationToken)
        {
            return Ok(await _userService.ConfirmEmailAsync(userId, code, cancellationToken));
        }

        /// <summary>
        /// Toggle User Status (Activate and Deactivate)
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("toggle-status")]
        public async Task<IActionResult> ToggleUserStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _userService.ToggleUserStatusAsync(request, cancellationToken));
        }
    }
}