using System.ComponentModel.DataAnnotations;

namespace Domain.Identity
{
    public class IdentityRequestToken
    {
        [Required(ErrorMessage = "Required AccessToken.")]
        public string AccessToken { get; set; }

        [Required(ErrorMessage = "Required RefreshToken.")]
        public string RefreshToken { get; set; }
    }
}
