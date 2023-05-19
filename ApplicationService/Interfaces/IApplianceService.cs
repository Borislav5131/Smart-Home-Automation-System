namespace ApplicationService.Interfaces
{
    using ApplicationService.DTOs.Appliance;

    public interface IApplianceService
    {
        Task<List<AllApplianceDTO>> GetAllAppliances();

        Task<AllApplianceDTO?> GetApplianceById(int id);

        Task<(bool added, string error)> Create(CreateApplianceDTO model);

        Task<EditApplianceDTO> GetEditDetailsOfAppliance(int id);

        Task<(bool eddited, string error)> Edit(int id, EditApplianceDTO model);

        Task<bool> Delete(int id);
    }
}
