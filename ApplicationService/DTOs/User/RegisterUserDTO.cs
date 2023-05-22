namespace ApplicationService.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserDTO
    {
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50), MinLength(2)]
        public string Password { get; set; }
    }
}
