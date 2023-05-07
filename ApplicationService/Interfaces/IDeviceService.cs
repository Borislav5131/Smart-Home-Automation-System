namespace ApplicationService.Interfaces
{
    using ApplicationService.DTOs.Device;

    public interface IDeviceService
    {
        Task<List<AllDeviceDTO>> GetAllDevices();

        Task<AllDeviceDTO?> GetDeviceById(int id);

        Task<(bool added, string error)> Create(CreateDeviceDTO model);

        Task<(bool eddited, string error)> Edit(int id, EditDeviceDTO model);

        Task<bool> Delete(int id);
    }
}
