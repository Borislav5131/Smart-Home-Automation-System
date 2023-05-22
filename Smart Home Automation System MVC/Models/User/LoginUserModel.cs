namespace Smart_Home_Automation_System_MVC.Models.User
{
    using Microsoft.Build.Framework;

    public class LoginUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
