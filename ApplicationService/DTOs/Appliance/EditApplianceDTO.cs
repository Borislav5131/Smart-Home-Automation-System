namespace ApplicationService.DTOs.Appliance
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Appliance;

    public  class EditApplianceDTO
    {
        [Required]
        [MaxLength(ApplianceNameMaxLenght, ErrorMessage = "Name must be max 50 characters!"), MinLength(ApplianceNameMinLenght, ErrorMessage = "Name must be min 5 characters!")]
        public string Name { get; set; }

        [Required]
        [MaxLength(ApplianceModelMaxLenght, ErrorMessage = "Model must be max 50 characters!"), MinLength(ApplianceModelMinLenght, ErrorMessage = "Model must be min 5 characters!")]
        public string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Range(AppliancePriceMinValue, AppliancePriceMaxValue, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }

        public bool IsTurnOn { get; set; }
    }
}
