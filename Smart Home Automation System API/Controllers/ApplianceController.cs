namespace Smart_Home_Automation_System_API.Controllers
{
    using ApplicationService.DTOs.Appliance;
    using ApplicationService.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplianceController : Controller
    {
        private readonly IApplianceService applianceService;

        public ApplianceController(IApplianceService applianceService)
        {
            this.applianceService = applianceService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var appliances = await applianceService.GetAllAppliances();

            return Ok(appliances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var appliance = await applianceService.GetApplianceById(id);

            if (appliance == null)
            {
                return NotFound();
            }

            return Ok(appliance);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateApplianceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (created, error) = await applianceService.Create(model);

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
            var appliance = await this.applianceService.GetEditDetailsOfAppliance(id);

            if (appliance == null)
            {
                return NotFound();
            }

            return Ok(appliance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditApplianceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (eddited, error) = await applianceService.Edit(id, model);

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
            var deleted = await applianceService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
