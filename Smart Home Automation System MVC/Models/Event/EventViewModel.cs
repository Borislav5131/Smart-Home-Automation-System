namespace Smart_Home_Automation_System_MVC.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        //public int DeviceId { get; set; }
    }
}
