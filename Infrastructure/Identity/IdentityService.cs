using Application.Identity;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResponseToken> GetIdentityResponseTokenAsync(IdentityLogin login)
        {
            var response = new IdentityResponseToken();

            if (login == null)
            {
                response.IsAuthenticated = false;
                response.Message = "Invalid login.";
                return response;
            }

            var user = await _userManager.FindByEmailAsync(login.UserName);

            if (user == null)
            {
                response.IsAuthenticated = false;
                response.Message = $"{login.UserName} is not registered in system.";
                return response;
            }

            if (await _userManager.CheckPasswordAsync(user, login.Password))
            {
                JwtSecurityToken jwtSecurityToken = await GetJwtSecurityTokenAsync(user);

                string _refreshToken = GenerateRefreshToken();
                string _accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                DateTime _refreshTokenExpiryTime = DateTime.Now.AddDays(30);


                //Updated to db
                user.RefreshToken = _refreshToken;
                user.RefreshTokenExpiryTime = _refreshTokenExpiryTime;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    response.IsAuthenticated = false;
                    response.Message = "Can not updated RefreshToken.";
                    return response;
                }

                response.IsAuthenticated = true;
                response.AccessToken = _accessToken;
                response.RefreshToken = _refreshToken;
                response.RefreshTokenExpiration = _refreshTokenExpiryTime;
                response.Email = user.Email;
                response.UserName = user.UserName;
                var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = roleList.ToList();
                return response;
            }

            response.IsAuthenticated = false;
            response.Message = $"Incorrect Credentials for user {user.UserName}";

            return response;
        }
        public async Task<IdentityResponseToken> GetIdentityResponseTokenAsync(IdentityRequestToken token)
        {
            var response = new IdentityResponseToken();

            if (token == null)
            {
                response.IsAuthenticated = false;
                response.Message = "Invalid IdentityRequestToken.";
                return response;
            }

            var principal = GetClaimsPrincipalByToken(token.AccessToken);

            if (principal == null)
            {
                response.IsAuthenticated = false;
                response.Message = "Invalid AccessToken.";
                return response;
            }

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user == null ||
                user.RefreshToken != token.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                response.IsAuthenticated = false;
                response.Message = "Invalid RefreshToken.";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GetJwtSecurityTokenAsync(user);
            string _refreshToken = GenerateRefreshToken();
            string _accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            //Updated to db
            user.RefreshToken = _refreshToken;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                response.IsAuthenticated = false;
                response.Message = "Can not updated RefreshToken.";
                return response;
            }

            response.IsAuthenticated = true;
            response.AccessToken = _accessToken;
            response.RefreshToken = _refreshToken;
            response.RefreshTokenExpiration = user.RefreshTokenExpiryTime;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = roleList.ToList();

            return response;
        }
        public async Task RevokeRefreshToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _userManager.UpdateAsync(user);
            }
        }
        private async Task<JwtSecurityToken> GetJwtSecurityTokenAsync(ApplicationUser user)
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
                new Claim(JwtRegisteredClaimNames.Name,user.Email),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Email),
                new Claim("Guid",user.Id.ToString())
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
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private ClaimsPrincipal GetClaimsPrincipalByToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid AccessToken");

            return principal;

        }

    }
}
