namespace Smart_Home_Automation_System_MVC.Services
{
    using Newtonsoft.Json;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.Device;
    using System.Reflection;

    public class DeviceService : IDeviceService
    {
        Uri baseAddress = new Uri("https://localhost:7119/api/Device");
        private readonly HttpClient client;

        public DeviceService()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public async Task<List<DeviceViewModel>> GetDevices()
        {
            List<DeviceViewModel> devices = new List<DeviceViewModel>();
            HttpResponseMessage response = await client.GetAsync($"{baseAddress}/All");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                devices = JsonConvert.DeserializeObject<List<DeviceViewModel>>(data);
            }

            return devices;
        }

        public async Task<bool> CreateDevice(CreateDeviceModel model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}/Create", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<EditDeviceModel> GetEditDeviceModel(int deviceId)
        {
            EditDeviceModel model = new EditDeviceModel();
            HttpResponseMessage response = await client.GetAsync($"{baseAddress}/GetEditDetails/{deviceId}");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<EditDeviceModel>(data);
                model.Id = deviceId;
            }

            return model;
        }

        public async Task<bool> EditDevice(EditDeviceModel model)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{baseAddress}/Edit/{model.Id}", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteDevice(int id)
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
