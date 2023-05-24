namespace ApplicationService.Services
{
    using ApplicationService.DTOs.Event;
    using ApplicationService.Interfaces;
    using Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EventService : IEventService
    {
        private readonly IRepository repo;

        public EventService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<AllEventDTO>> GetAllEventsOfDevice(int deviceId, string search)
        {
            var events = await repo.All<Event>();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                events = events.Where(d => d.Name.ToLower().Contains(search));
            }

            var result = events
                .Where(e => e.DeviceId == deviceId)
                .Select(e => new AllEventDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Description = e.Description,
                    IsActive = e.IsActive,
                    DeviceId = e.DeviceId
                })
                .OrderBy(e => e.Name)
                .ToList();

            return result;
        }

        public async Task<AllEventDTO?> GetEventById(int id)
        {
            var events = await repo.All<Event>();
            var result = events
                .Where(e => e.Id == id)
                .Select(e => new AllEventDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Description = e.Description,
                    IsActive = e.IsActive,
                    DeviceId = e.DeviceId
                })
                .FirstOrDefault();

            return result;
        }

        public async Task<(bool added, string error)> Create(CreateEventDTO model)
        {
            bool added = false;
            string error = null;

            var devices = await repo.All<Device>();
            var device = devices.FirstOrDefault(d => d.Id == model.DeviceId);

            if (device == null)
            {
                return (added, error = "Something get wrong!");
            }

            Event @event = new Event()
            {
                Name = model.Name,
                Date = model.Date,
                Description = model.Description,
                IsActive = model.IsActive,
                DeviceId = model.DeviceId,
                Device = device,
            };

            try
            {
                device.Events.Add(@event);
                await repo.Add<Event>(@event);
                await repo.SaveChanges();
                added = true;
            }
            catch (Exception)
            {
                error = "Could not create event!";
            }

            return (added, error);
        }

        public async Task<(bool eddited, string error)> Edit(int id, EditEventDTO model)
        {
            bool eddited = false;
            string error = null;

            var devices = await repo.All<Device>();
            var device = devices.FirstOrDefault(d => d.Id == model.DeviceId);

            if (device == null)
            {
                return (eddited, error = "Something get wrong!");
            }

            var events = await repo.All<Event>();
            var @event = events.FirstOrDefault(e => e.Id == id);

            if (@event == null)
            {
                return (eddited, error = "Something get wrong!");
            }

            @event.Name = model.Name;
            @event.Date = model.Date;
            @event.Description = model.Description;
            @event.IsActive = model.IsActive;

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
            var events = await repo.All<Event>();
            var @event = events.FirstOrDefault(e => e.Id == id);

            if (@event == null)
            {
                return false;
            }

            var devices = await repo.All<Device>();
            var device = devices.FirstOrDefault(d => d.Id == @event.DeviceId);

            if (device == null)
            {
                return false;
            }

            try
            {
                device.Events.Remove(@event);
                await repo.Remove<Event>(@event);
                await repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EditEventDTO> GetEditDetailsOfEvent(int id)
        {
            var events = await repo.All<Event>();
            var @event = events
                .Where(e => e.Id == id)
                .Select(e => new EditEventDTO
                {
                    Name = e.Name,
                    Date = e.Date,
                    Description = e.Description,
                    IsActive = e.IsActive,
                    DeviceId = e.DeviceId,
                })
                .FirstOrDefault();

            return @event;
        }
    }
}
