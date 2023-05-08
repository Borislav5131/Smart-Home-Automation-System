﻿namespace Smart_Home_Automation_System_API.Controllers
{
    using ApplicationService.DTOs.Event;
    using ApplicationService.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> All(int deviceId)
        {
            var events = await eventService.GetAllEventsOfDevice(deviceId);

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var @event = await eventService.GetEventById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (created, error) = await eventService.Create(model);

            if (!created)
            {
                ModelState.AddModelError(string.Empty, error);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditEventDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (eddited, error) = await eventService.Edit(id, model);

            if (!eddited)
            {
                ModelState.AddModelError(string.Empty, error);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await eventService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}