namespace Smart_Home_Automation_System_MVC.Interfaces
{
    using Smart_Home_Automation_System_MVC.Models.Appliance;

    public interface IApplianceService
    {
        Task<List<ApplianceViewModel>> GetAppliances();

        Task<bool> CreateAppliance(CreateApplianceModel model);

        Task<EditApplianceModel> GetEditApplianceModel(int applianceId);

        Task<bool> EditAppliance(EditApplianceModel model);

        Task<bool> DeleteAppliance(int id);
    }
}
