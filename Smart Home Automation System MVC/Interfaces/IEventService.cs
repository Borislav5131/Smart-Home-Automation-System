namespace Smart_Home_Automation_System_MVC.Interfaces
{
    using Smart_Home_Automation_System_MVC.Models.Event;

    public interface IEventService
    {
        Task<List<EventViewModel>> GetEvents(int deviceId, string search);

        Task<bool> CreateEvent(CreateEventModel model);

        Task<EditEventModel> GetEditEventModel(int eventId);

        Task<bool> EditEvent(EditEventModel model);

        Task<bool> DeleteEvent(int id);
    }
}
