namespace ApplicationService.DTOs.Event
{
    using ApplicationService.DTOs.Device;
    using System;

    public class AllEventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int DeviceId { get; set; }
    }
}
