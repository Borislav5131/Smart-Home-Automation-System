namespace Smart_Home_Automation_System_MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smart_Home_Automation_System_MVC.Attributes;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.Appliance;
    using X.PagedList;

    [RedirectToLogin]
    public class ApplianceController : Controller
    {
        private readonly IApplianceService applianceService;

        public ApplianceController(IApplianceService applianceService)
        {
            this.applianceService = applianceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppliances(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var result = await this.applianceService.GetAppliances();

            var pagedData = result.ToPagedList(pageNumber, pageSize);

            return View(pagedData);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppliance() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAppliance(CreateApplianceModel createApplianceModel)
        {
            var created = await this.applianceService.CreateAppliance(createApplianceModel);

            if (created)
            {
                return RedirectToAction("GetAllAppliances");
            }

            return View(createApplianceModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditAppliance(int applianceId)
        {
            var model = await this.applianceService.GetEditApplianceModel(applianceId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppliance(EditApplianceModel editApplianceModel)
        {
            var edited = await this.applianceService.EditAppliance(editApplianceModel);

            if (edited)
            {
                return RedirectToAction("GetAllAppliances");
            }

            return View(editApplianceModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAppliance(int applianceId)
        {
            var deleted = await this.applianceService.DeleteAppliance(applianceId);

            return RedirectToAction("GetAllAppliances");
        }
    }
}
