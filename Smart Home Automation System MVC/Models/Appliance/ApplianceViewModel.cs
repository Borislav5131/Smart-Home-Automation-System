namespace Smart_Home_Automation_System_MVC.Models.Appliance
{
    public class ApplianceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Price { get; set; }

        public bool IsTurnOn { get; set; }
    }
}
