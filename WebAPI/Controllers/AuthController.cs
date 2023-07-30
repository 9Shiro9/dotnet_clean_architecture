using Application.Interfaces;
using Domain.DTOs;
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

        [HttpPost]
        public async Task<ActionResult<IdentityResponseDto>> GetTokenAsync(string userName, string password)
        {
            //var result = await _identityService.AuthorizeAsync(userName, password);
            
            return Ok(200);
        }
    }
}
