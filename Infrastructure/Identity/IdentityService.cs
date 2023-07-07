using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResponseDto> AuthorizeAsync(string username, string password)
        {
            var response = new IdentityResponseDto();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                response.IsAuthenticated = false;
                response.Message = $"{username} is not registered in system.";
                return response;
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                response.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.UserName = user.UserName;

                var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = roleList.ToList();

                return response;
            }

            response.IsAuthenticated = false;
            response.Message = $"Incorrect Credentials for user {user.Email}";

            return response;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id.ToString())
            }
            .Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:DurationInMinutes"])),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
