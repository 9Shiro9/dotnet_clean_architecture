using System.ComponentModel.DataAnnotations;

namespace Domain.Identity
{
    public class IdentityLogin
    {
        [Required(ErrorMessage = "Required UserName.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required Password.")]
        public string Password { get; set; }
    }
}
