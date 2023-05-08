namespace ApplicationService.DTOs.Appliance
{
    using System.ComponentModel.DataAnnotations;

    public class AllApplianceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Price { get; set; }

        public bool IsTurnOn { get; set; }

        //public List<Device> Devices { get; set; } = new List<Device>();
    }
}
