namespace ApplicationService.DTOs.Device
{
    public class AllDeviceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Location { get; set; }

        public float PowerUsage { get; set; }

        public string Image { get; set; }

        //public List<Event> Events { get; set; }

        //public List<Appliance> Appliances { get; set; }
    }
}
