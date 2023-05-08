﻿namespace ApplicationService.DTOs.Event
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Event;

    public  class EditEventDTO
    {
        [Required]
        [MaxLength(EventNameMaxLenght, ErrorMessage = "Name must be max 50 characters!"), MinLength(EventNameMinLenght, ErrorMessage = "Name must be min 5 characters!")]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(EventDescriptionMaxLenght, ErrorMessage = "Description must be max 150 characters!"), MinLength(EventDescriptionMinLenght, ErrorMessage = "Description must be min 10 characters!")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int DeviceId { get; set; }
    }
}
