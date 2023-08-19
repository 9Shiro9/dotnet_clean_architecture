using Domain.Identity;

namespace Application.Identity
{
    public interface IIdentityService
    {
        Task<IdentityResponseToken> GetIdentityResponseTokenAsync(IdentityLogin login);

        Task<IdentityResponseToken> GetIdentityResponseTokenAsync(IdentityRequestToken token);

        Task RevokeRefreshToken(string userId);
    }
}
