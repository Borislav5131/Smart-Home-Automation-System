namespace ApplicationService.Services
{
    using ApplicationService.DTOs.Device;
    using ApplicationService.Interfaces;
    using Data.Entities;
    using System.Collections.Generic;

    public class DeviceService : IDeviceService
    {
        private readonly IRepository repo;

        public DeviceService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<AllDeviceDTO>> GetAllDevices(string search)
        {
            var devices = await repo.All<Device>();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                devices = devices.Where(d => d.Name.ToLower().Contains(search));
            }

            var result = devices
                .Select(d => new AllDeviceDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.Type,
                    Status = d.Status,
                    Location = d.Location,
                    PowerUsage = d.PowerUsage,
                    Image = "data:image;base64," + Convert.ToBase64String(d.Image)
                })
                .OrderBy(d => d.Name)
                .ToList();

            return result;
        }

        public async Task<AllDeviceDTO?> GetDeviceById(int id)
        {
            var devices = await repo.All<Device>();
            var result = devices
                .Where(d => d.Id == id)
                .Select(d => new AllDeviceDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.Type,
                    Status = d.Status,
                    Location = d.Location,
                    PowerUsage = d.PowerUsage,
                    Image = "data:image;base64," + Convert.ToBase64String(d.Image)
                })
                .FirstOrDefault();

            return result;
        }

        public async Task<(bool added, string error)> Create(CreateDeviceDTO model)
        {
            bool added = false;
            string error = null;

            Device device = new Device() 
            {
                Name = model.Name,
                Type = model.Type,
                Status = model.Status,
                Location = model.Location,
                PowerUsage = model.PowerUsage,
                Image = model.Image,
            };

            try
            {
                await repo.Add<Device>(device);
                await repo.SaveChanges();
                added = true;
            }
            catch (Exception)
            {
                error = "Could not create device!";
            }

            return (added, error);
        }

        public async Task<(bool eddited, string error)> Edit(int id, EditDeviceDTO model)
        {
            bool eddited = false;
            string error = null;

            var devices = await repo.All<Device>();
            var device = devices.Where(d => d.Id == id).FirstOrDefault();

            if(device == null)
            {
                return (eddited, error = "Something get wrong!");
            }

            device.Name = model.Name;
            device.Type = model.Type;
            device.Status = model.Status;
            device.Location = model.Location;
            device.PowerUsage = model.PowerUsage;
            device.Image = model.Image;

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
            var devices = await repo.All<Device>();
            var device = devices.Where(d => d.Id == id).FirstOrDefault();

            if (device == null)
            {
                return false;
            }

            try
            {
                await repo.Remove<Device>(device);
                await repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EditDeviceDTO> GetEditDetailsOfDevice(int id)
        {
            var devices = await repo.All<Device>();
            var device = devices
                .Where(d => d.Id == id)
                .Select(d => new EditDeviceDTO
                {
                    Name = d.Name,
                    Type = d.Type,
                    Status = d.Status,
                    Location = d.Location,
                    PowerUsage = d.PowerUsage,
                    Image = d.Image,
                })
                .FirstOrDefault();

            return device;
        }
    }
}
