namespace Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Appliance;

    public class Appliance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ApplianceNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ApplianceModelMaxLenght)]
        public string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Range(AppliancePriceMinValue, AppliancePriceMaxValue)]
        public decimal Price { get; set; }

        public bool IsTurnOn { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();
    }
}
