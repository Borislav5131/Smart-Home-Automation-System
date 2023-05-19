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

        public async Task<List<AllApplianceDTO>> GetAllAppliances()
        {
            var appliances = await repo.All<Appliance>();
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

            var appliances = await repo.All<Appliance>();
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
            var appliances = await repo.All<Appliance>();
            var appliance = appliances
                .Where(a => a.Id == id)
                .Select(a => new EditApplianceDTO
                {
                    Name = a.Name,
                    IsTurnOn = a.IsTurnOn,
                    Model = a.Model,
                    Price = a.Price,
                    PurchaseDate = a.PurchaseDate,
                })
                .FirstOrDefault();

            return appliance;
        }
    }
}
