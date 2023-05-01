namespace Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Data.DataConstants.Event;

    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLenght)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(EventDescriptionMaxLenght)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public Device Device { get; set; }
    }
}
