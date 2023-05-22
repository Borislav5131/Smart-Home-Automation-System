namespace ApplicationService.Services
{
    using ApplicationService.DTOs.User;
    using ApplicationService.Interfaces;
    using Data.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IRepository repo;

        public UserService(IRepository repo, IConfiguration configuration)
        {
            this.repo = repo;
            this.configuration = configuration;
        }

        public async Task<bool> Login(LoginUserDTO loginUserDTO)
        {
            var users = await repo.All<User>();
            var result = users
                .Where(u => u.Username == loginUserDTO.Username && u.Password == loginUserDTO.Password)
                .FirstOrDefault();

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Register(RegisterUserDTO registerUserDTO)
        {
            var users = await repo.All<User>();
            var isUsernameExist = users.Any(u => u.Username == registerUserDTO.Username);

            if (isUsernameExist)
            {
                return false;
            }

            var user = new User()
            {
                Username = registerUserDTO.Username,
                Password = registerUserDTO.Password,
            };

            try
            {
                await repo.Add<User>(user);
                await repo.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<string> GenerateJwtToken(LoginUserDTO loginUserDTO)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginUserDTO.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
