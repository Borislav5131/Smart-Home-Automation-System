namespace Smart_Home_Automation_System_MVC.Services
{
    using Newtonsoft.Json;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.Appliance;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ApplianceService : IApplianceService
    {
        Uri baseAddress = new Uri("https://localhost:7119/api/Appliance");
        private readonly HttpClient client;

        public ApplianceService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("MyAPI");
            client.BaseAddress = baseAddress;
        }

        public async Task<List<ApplianceViewModel>> GetAppliances(string search)
        {
            List<ApplianceViewModel> appliances = new List<ApplianceViewModel>();
            HttpResponseMessage response = await client.GetAsync($"{baseAddress}/All?search={search}");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                appliances = JsonConvert.DeserializeObject<List<ApplianceViewModel>>(data);
            }

            return appliances;
        }

        public async Task<bool> CreateAppliance(CreateApplianceModel model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}/Create", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<EditApplianceModel> GetEditApplianceModel(int applianceId)
        {
            EditApplianceModel model = new EditApplianceModel();
            HttpResponseMessage response = await client.GetAsync($"{baseAddress}/GetEditDetails/{applianceId}");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<EditApplianceModel>(data);
                model.Id = applianceId;
            }

            return model;
        }

        public async Task<bool> EditAppliance(EditApplianceModel model)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{baseAddress}/Edit/{model.Id}", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAppliance(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{baseAddress}/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
