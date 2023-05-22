namespace Smart_Home_Automation_System_MVC.Services
{
    using Newtonsoft.Json;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.Event;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EventService : IEventService
    {
        Uri baseAddress = new Uri("https://localhost:7119/api/Event");
        private readonly HttpClient client;

        public EventService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("MyAPI");
            client.BaseAddress = baseAddress;
        }

        public async Task<List<EventViewModel>> GetEvents(int deviceId)
        {
            List<EventViewModel> events = new List<EventViewModel>();
            HttpResponseMessage response = await client.GetAsync($"{baseAddress}/All/{deviceId}");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                events = JsonConvert.DeserializeObject<List<EventViewModel>>(data);
            }

            return events;
        }

        public async Task<bool> CreateEvent(CreateEventModel model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}/Create", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<EditEventModel> GetEditEventModel(int eventId)
        {
            EditEventModel model = new EditEventModel();
            HttpResponseMessage response = await client.GetAsync($"{baseAddress}/GetEditDetails/{eventId}");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<EditEventModel>(data);
                model.Id = eventId;
            }

            return model;
        }

        public async Task<bool> EditEvent(EditEventModel model)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{baseAddress}/Edit/{model.Id}", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteEvent(int id)
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
