using System.ComponentModel.DataAnnotations;

namespace ChatApp.Blazor.Data
{
    public class SignInModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
