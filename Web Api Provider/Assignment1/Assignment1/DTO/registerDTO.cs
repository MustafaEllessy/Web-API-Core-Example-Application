using System.ComponentModel.DataAnnotations;

namespace Assignment1.DTO
{
    public class registerDTO
    {
     
        [Required]

        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [Compare("Password",   ErrorMessage ="Not comptible passwords")]
        public string ConfirmPassword { get; set; }
        [Required]

        public string Email { get; set; }

    }
}
