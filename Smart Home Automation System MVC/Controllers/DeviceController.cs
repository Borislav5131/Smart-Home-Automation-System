﻿namespace Smart_Home_Automation_System_MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smart_Home_Automation_System_MVC.Attributes;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.Device;

    [RedirectToLogin]
    public class DeviceController : Controller
    {
        private readonly IDeviceService deviceService;

        public DeviceController(IDeviceService deviceService)
        {
           this.deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDevices()
        {
            var result = await this.deviceService.GetDevices();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDevice() => View();

        [HttpPost]
        public async Task<IActionResult> CreateDevice(CreateDeviceModel model, IFormFile image)
        {
            var convertedImage = ConvertImageToBytes(image);
            model.Image = convertedImage;

            var created = await this.deviceService.CreateDevice(model);

            if (created)
            {
                return RedirectToAction("GetAllDevices");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditDevice(int deviceId)
        {
            var model = await this.deviceService.GetEditDeviceModel(deviceId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDevice(EditDeviceModel model, IFormFile image)
        {
            if (image != null)
            {
                var convertedImage = ConvertImageToBytes(image);
                model.Image = convertedImage;
            }

            var edited = await this.deviceService.EditDevice(model);

            if (edited)
            {
                return RedirectToAction("GetAllDevices");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            var deleted = await this.deviceService.DeleteDevice(deviceId);

            return RedirectToAction("GetAllDevices");
        }

        private byte[] ConvertImageToBytes(IFormFile image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);

            return ms.ToArray();
        }
    }
}
