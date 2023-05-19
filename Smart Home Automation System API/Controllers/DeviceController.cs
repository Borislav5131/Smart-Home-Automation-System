namespace Smart_Home_Automation_System_API.Controllers
{
    using ApplicationService.DTOs.Device;
    using ApplicationService.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly IDeviceService deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var devices = await deviceService.GetAllDevices();

            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var device = await deviceService.GetDeviceById(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateDeviceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (created, error) = await deviceService.Create(model);

            if (!created)
            {
                ModelState.AddModelError(string.Empty, error);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditDetails([FromRoute]int id)
        {
            var device = await this.deviceService.GetEditDetailsOfDevice(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditDeviceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (eddited, error) = await deviceService.Edit(id, model);

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
            var deleted = await deviceService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
