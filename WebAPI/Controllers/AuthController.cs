using Application.Identity;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<IdentityResponseToken>> GetTokenByLoginAsync(IdentityLogin login)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.GetIdentityResponseTokenAsync(login);

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("token")]
        public async Task<ActionResult<IdentityResponseToken>> GetTokenByRefreshTokenAsync(IdentityRequestToken token)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.GetIdentityResponseTokenAsync(token);

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _identityService.RevokeRefreshToken(userId);

                return Ok();
            }

            return BadRequest();
        }
    }
}
