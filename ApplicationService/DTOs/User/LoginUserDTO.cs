namespace ApplicationService.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
