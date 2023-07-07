namespace Domain.DTOs
{
    public class IdentityResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
