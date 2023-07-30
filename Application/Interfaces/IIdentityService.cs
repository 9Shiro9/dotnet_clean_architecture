using Application.Identity;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string> RegisterAsync(ApplicationIdentityRegister register);
        Task<ApplicationIdentityTokenResponse> GetTokenAsync(ApplicationIdentityTokenRequest request);
        Task<ApplicationIdentityTokenResponse> GetRefreshTokenAsync(string jwtToken);
    }
}
