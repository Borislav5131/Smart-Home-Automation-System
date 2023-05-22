namespace ApplicationService.Interfaces
{
    using ApplicationService.DTOs.User;

    public interface IUserService
    {
        Task<bool> Register(RegisterUserDTO registerUserDTO);

        Task<bool> Login(LoginUserDTO loginUserDTO);

        Task<string> GenerateJwtToken(LoginUserDTO loginUserDTO);
    }
}
