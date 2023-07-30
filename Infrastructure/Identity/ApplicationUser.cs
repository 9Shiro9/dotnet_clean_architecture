using Domain.Common;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid> , IBaseEntity
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
