namespace Smart_Home_Automation_System_MVC.Models
{
    public class Constants
    {
        public class Device
        {
            public const int DeviceNameMaxLenght = 50;
            public const int DeviceNameMinLenght = 5;

            public const int DeviceTypeMaxLenght = 20;
            public const int DeviceTypeMinLenght = 3;

            public const int DeviceStatusMaxLenght = 20;
            public const int DeviceStatusMinLenght = 3;

            public const int DeviceLocationMaxLenght = 20;
            public const int DeviceLocationMinLenght = 3;

            public const double DevicePowerUsageMaxValue = 100000;
            public const double DevicePowerUsageMinValue = 0;

            public const int DeviceMaxImageSize = 2 * 1024 * 1024;
        }

        public class Event
        {
            public const int EventNameMaxLenght = 50;
            public const int EventNameMinLenght = 5;

            public const int EventDescriptionMaxLenght = 150;
            public const int EventDescriptionMinLenght = 10;
        }

        public class Appliance
        {
            public const int ApplianceNameMaxLenght = 50;
            public const int ApplianceNameMinLenght = 5;

            public const int ApplianceModelMaxLenght = 50;
            public const int ApplianceModelMinLenght = 5;

            public const double AppliancePriceMaxValue = 100000;
            public const double AppliancePriceMinValue = 0;
        }
    }
}
