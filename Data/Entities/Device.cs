namespace Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Device;

    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DeviceNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DeviceTypeMaxLenght)]
        public string Type { get; set; }

        [MaxLength(DeviceStatusMaxLenght)]
        public string Status { get; set; }

        [MaxLength(DeviceLocationMaxLenght)]
        public string Location { get; set; }

        [Range(DevicePowerUsageMinValue, DevicePowerUsageMaxValue)]
        public float PowerUsage { get; set; }

        [MaxLength(DeviceMaxImageSize)]
        public byte[] Image { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();

        public List<Appliance> Appliances { get; set; } = new List<Appliance>();
    }
}
