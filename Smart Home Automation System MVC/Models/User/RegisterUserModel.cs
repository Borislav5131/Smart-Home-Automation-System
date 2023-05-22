namespace Smart_Home_Automation_System_MVC.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserModel
    {
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50), MinLength(2)]
        public string Password { get; set; }
    }
}
