namespace Smart_Home_Automation_System_MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smart_Home_Automation_System_MVC.Attributes;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.Event;

    [RedirectToLogin]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(int deviceId)
        {
            var result = await this.eventService.GetEvents(deviceId);
            ViewBag.DeviceId = deviceId;

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEvent(int deviceId)
        {
            CreateEventModel model = new CreateEventModel();
            model.DeviceId = deviceId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventModel model)
        {
            var created = await this.eventService.CreateEvent(model);

            if (created)
            {
                return RedirectToAction("GetAllEvents", new { deviceId = model.DeviceId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(int eventId)
        {
            var model = await this.eventService.GetEditEventModel(eventId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EditEventModel model)
        {
            var edited = await this.eventService.EditEvent(model);

            if (edited)
            {
                return RedirectToAction("GetAllEvents", new { deviceId = model.DeviceId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(int eventId, int deviceId)
        {
            var deleted = await this.eventService.DeleteEvent(eventId);

            return RedirectToAction("GetAllEvents", new { deviceId = deviceId });
        }
    }
}
