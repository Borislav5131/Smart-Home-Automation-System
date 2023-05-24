namespace Smart_Home_Automation_System_MVC.Interfaces
{
    using Smart_Home_Automation_System_MVC.Models.Device;

    public interface IDeviceService
    {
        Task<List<DeviceViewModel>> GetDevices(string search);

        Task<bool> CreateDevice(CreateDeviceModel model);

        Task<EditDeviceModel> GetEditDeviceModel(int deviceId);

        Task<bool> EditDevice(EditDeviceModel model);

        Task<bool> DeleteDevice(int id);
    }
}
