namespace ApplicationService.Interfaces
{
    using ApplicationService.DTOs.Device;
    using ApplicationService.DTOs.Event;

    public interface IEventService
    {
        Task<List<AllEventDTO>> GetAllEventsOfDevice(int deviceId);

        Task<AllEventDTO?> GetEventById(int id);

        Task<(bool added, string error)> Create(CreateEventDTO model);

        Task<EditEventDTO> GetEditDetailsOfEvent(int id);

        Task<(bool eddited, string error)> Edit(int id, EditEventDTO model);

        Task<bool> Delete(int id);
    }
}
