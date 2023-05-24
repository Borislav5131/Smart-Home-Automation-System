namespace Smart_Home_Automation_System_API.Controllers
{
    using ApplicationService.DTOs.Event;
    using ApplicationService.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> All(int deviceId, string? search)
        {
            var events = await eventService.GetAllEventsOfDevice(deviceId, search);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditDetails([FromRoute] int id)
        {
            var @event = await this.eventService.GetEditDetailsOfEvent(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
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
