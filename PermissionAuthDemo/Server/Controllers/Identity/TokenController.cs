using Microsoft.AspNetCore.Mvc;
using PermissionAuthDemo.Server.Services.Token;
using PermissionAuthDemo.Shared.Requests.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Server.Controllers.Identity
{
    [Route("api/identity/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _identityService;

        public TokenController(ITokenService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Get Token (Email, Password)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost]
        public async Task<ActionResult> Get(TokenRequest model, CancellationToken cancellationToken)
        {
            var response = await _identityService.LoginAsync(model, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest model, CancellationToken cancellationToken)
        {
            var response = await _identityService.GetRefreshTokenAsync(model, cancellationToken);
            return Ok(response);
        }
    }
}