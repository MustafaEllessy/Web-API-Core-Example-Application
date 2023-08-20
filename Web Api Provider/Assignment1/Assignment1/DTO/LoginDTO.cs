using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Assignment1.DTO
{
    public class LoginDTO
    {
        [Required]
        public string userName {  get; set; }
        [Required]
        public string password { get; set; }    
    }
}
