namespace Smart_Home_Automation_System_MVC.Interfaces
{
    using Smart_Home_Automation_System_MVC.Models.User;

    public interface IUserService
    {
        Task<string> Login(LoginUserModel loginUserModel);

        Task<bool> Register(RegisterUserModel registerUserModel);
    }
}
