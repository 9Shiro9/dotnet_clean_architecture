using Application.Identity;
using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Domain.Identity;
using Domain.Interfaces;
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
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository;
        private readonly IBaseRepository<ApplicationUser> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IBaseRepository<RefreshToken> refreshTokenRepository, IBaseRepository<ApplicationUser> userRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<ApplicationIdentityTokenResponse> GetRefreshTokenAsync(string jwtToken)
        {
            var response = new ApplicationIdentityTokenResponse();
            var user = await _userRepository.GetAsync(x => x.RefreshTokens.Any(t => t.Token == jwtToken));

            if (user == null)
            {
                response.IsAuthenticated = false;
                response.Message = $"Token did not match any users.";
                return response;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == jwtToken);

            if (!refreshToken.IsActive)
            {
                response.IsAuthenticated = false;
                response.Message = $"Token Not Active.";
                return response;
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();

            response.RefreshToken = refreshToken.Token;
            response.RefreshTokenExpiration = refreshToken.Expires;

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            //Generates new jwt
            response.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.RefreshToken = newRefreshToken.Token;
            response.RefreshTokenExpiration = newRefreshToken.Expires;
            return response;
        }

        public async Task<ApplicationIdentityTokenResponse> GetTokenAsync(ApplicationIdentityTokenRequest request)
        {
            var response = new ApplicationIdentityTokenResponse();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.IsAuthenticated = false;
                response.Message = $"No Accounts Registered with {request.Email}.";
                return response;
            }

            if(await _userManager.CheckPasswordAsync(user,request.Password))
            {

                response.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();


                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);

                    if(activeRefreshToken != null)
                    {
                        response.RefreshToken = activeRefreshToken.Token;
                        response.RefreshTokenExpiration = activeRefreshToken.Expires;
                    }
                   
                }
                else
                {
                    var refreshToken = CreateRefreshToken();
                    response.RefreshToken = refreshToken.Token;
                    response.RefreshTokenExpiration = refreshToken.Expires;

                    await _refreshTokenRepository.AddAsync(refreshToken);
                    await _unitOfWork.SaveChangesAsync();
                }

                return response;

            }

            response.IsAuthenticated = false;
            response.Message = $"Incorrect Credentials for user {user.Email}.";

            return response;
        }

        public async Task<string> RegisterAsync(ApplicationIdentityRegister register)
        {
            var user = new ApplicationUser
            {
                UserName = register.Email,
                Email = register.Email
            };

            var checkUserEmail = await _userManager.FindByEmailAsync(register.Email);

            if (checkUserEmail != null)
            {
                return $"Email {register.Email} is already registered.";
            }

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.ROLE_USER.ToString());
            }
            return $"User Registered with username {user.UserName}";

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

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };

            }
        }
    }
}
