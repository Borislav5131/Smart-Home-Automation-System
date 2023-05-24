namespace ApplicationService.Services
{
    using ApplicationService.DTOs.Appliance;
    using ApplicationService.Interfaces;
    using Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ApplianceService : IApplianceService
    {
        private readonly IRepository repo;

        public ApplianceService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<AllApplianceDTO>> GetAllAppliances(string search)
        {
            var appliances = await repo.All<Appliance>();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                appliances = appliances.Where(d => d.Name.ToLower().Contains(search));
            }

            var result = appliances
                .Select(a => new AllApplianceDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsTurnOn = a.IsTurnOn,
                    Model = a.Model,
                    Price = a.Price,
                    PurchaseDate = a.PurchaseDate,
                })
                .OrderBy(a => a.Name)
                .ToList();

            return result;
        }

        public async Task<AllApplianceDTO?> GetApplianceById(int id)
        {
            var appliances = await repo.All<Appliance>();
            var result = appliances
                .Where(a => a.Id == id)
                .Select(a => new AllApplianceDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsTurnOn = a.IsTurnOn,
                    Model = a.Model,
                    Price = a.Price,
                    PurchaseDate = a.PurchaseDate,
                })
                .FirstOrDefault();

            return result;
        }

        public async Task<(bool added, string error)> Create(CreateApplianceDTO model)
        {
            bool added = false;
            string error = null;

            Appliance appliance = new Appliance() 
            {
                Name = model.Name,
                IsTurnOn = model.IsTurnOn,
                Model = model.Model,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate,
            };

            try
            {
                await repo.Add<Appliance>(appliance);
                await repo.SaveChanges();
                added = true;
            }
            catch (Exception)
            {
                error = "Could not create device!";
            }

            return (added, error);
        }

        public async Task<(bool eddited, string error)> Edit(int id, EditApplianceDTO model)
        {
            bool eddited = false;
            string error = null;

            var appliances = await repo.AllIncluding<Appliance>(a => a.Devices);
            var appliance = appliances.Where(d => d.Id == id).FirstOrDefault();

            if (appliance == null)
            {
                return (eddited, error = "Something get wrong!");
            }

            appliance.Name = model.Name;
            appliance.Model = model.Model;
            appliance.Price = model.Price;
            appliance.PurchaseDate = model.PurchaseDate;
            appliance.IsTurnOn = model.IsTurnOn;

            if (model.SelectedDeviceIds != null)
            {
                var devices = await repo.All<Device>();
                var selectedDevices = devices.Where(device => model.SelectedDeviceIds.Contains(device.Id)).ToList();

                foreach (var device in selectedDevices)
                {
                    if (!appliance.Devices.Contains(device))
                    {
                        appliance.Devices.Add(device);
                    }
                }

                var removedDevices = appliance.Devices.Except(selectedDevices).ToList();
                foreach (var device in removedDevices)
                {
                    appliance.Devices.Remove(device);
                }
            }
            else
            {
                appliance.Devices.Clear();
            }

            try
            {
                await repo.SaveChanges();
                eddited = true;
            }
            catch (Exception)
            {
                error = "Something get wrong!";
            }

            return (eddited, error);
        }

        public async Task<bool> Delete(int id)
        {
            var appliances = await repo.All<Appliance>();
            var appliance = appliances.Where(d => d.Id == id).FirstOrDefault();

            if (appliance == null)
            {
                return false;
            }

            try
            {
                await repo.Remove<Appliance>(appliance);
                await repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EditApplianceDTO> GetEditDetailsOfAppliance(int id)
        {
            var devices = await repo.All<Device>();
            var deviceOptions = devices.Select(d => new SelectListItemDTO
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });

            var appliances = await repo.AllIncluding<Appliance>(a => a.Devices);
            var appliance = appliances
                .Where(a => a.Id == id)
                .Select(a => new EditApplianceDTO
                {
                    Name = a.Name,
                    IsTurnOn = a.IsTurnOn,
                    Model = a.Model,
                    Price = a.Price,
                    PurchaseDate = a.PurchaseDate,
                    SelectedDeviceIds = a.Devices.Select(d => d.Id).ToList(),
                    DeviceOptions = deviceOptions
                })
                .FirstOrDefault();

            return appliance;
        }
    }
}
