namespace Smart_Home_Automation_System_MVC.Models.Device
{
    using System.ComponentModel.DataAnnotations;
    using static Constants.Device;

    public class EditDeviceModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(DeviceNameMaxLenght, ErrorMessage = "Name must be max 50 characters!"), MinLength(DeviceNameMinLenght, ErrorMessage = "Name must be min 5 characters!")]
        public string Name { get; set; }

        [Required]
        [MaxLength(DeviceTypeMaxLenght, ErrorMessage = "Type must be max 20 characters!"), MinLength(DeviceTypeMinLenght, ErrorMessage = "Type must be min 3 characters!")]
        public string Type { get; set; }

        [MaxLength(DeviceStatusMaxLenght, ErrorMessage = "Status must be max 20 characters!"), MinLength(DeviceStatusMinLenght, ErrorMessage = "Status must be min 3 characters!")]
        public string Status { get; set; }

        [MaxLength(DeviceLocationMaxLenght, ErrorMessage = "Location must be max 20 characters!"), MinLength(DeviceLocationMinLenght, ErrorMessage = "Location must be min 3 characters!")]
        public string Location { get; set; }

        [Range(DevicePowerUsageMinValue, DevicePowerUsageMaxValue, ErrorMessage = "Power Usage must be between 0 and 100000")]
        public float PowerUsage { get; set; }

        [MaxLength(DeviceMaxImageSize)]
        public byte[] Image { get; set; }
    }
}
