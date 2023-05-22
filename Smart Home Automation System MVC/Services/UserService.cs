namespace Smart_Home_Automation_System_MVC.Services
{
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.User;
    using System.Net.Http.Headers;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        Uri baseAddress = new Uri("https://localhost:7119/api/Auth");
        private readonly HttpClient client;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("MyAPI");
            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public  async Task<string> Login(LoginUserModel loginUserModel)
        {
            string token = null;

            HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}/Login", loginUserModel);

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsStringAsync();
            }

            return token;
        }

        public async Task<bool> Register(RegisterUserModel registerUserModel)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}/Register", registerUserModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
