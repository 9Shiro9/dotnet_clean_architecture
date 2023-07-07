using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResponseDto> AuthorizeAsync(string username, string password);

    }
}
