using Infrastructure.Contract; 
using Infrastructure.Domain;
using Infrastructure.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Command;
using System.Net;

namespace FnbPrintingMiddleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;

        private readonly INotificationClient _notificationClient;


        public NotificationController(ILogger<NotificationController> logger,
            INotificationClient notificationService)
        {
            _logger = logger;
            _notificationClient = notificationService;
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NotifyAsync([FromBody] NotificationCommand command)
        {
            try
            {
                await _notificationClient.SendNotificationAsync(command);

                return Ok("sent");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Exception in OnBoardingController, CreateOrganization", exception);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Notify()
        //{
        //    try
        //    {
        //        string message = "ekti message";
        //        await _notificationService.SendMessageToAllAsync(message);
        //        //var sendToQueueResponse = serviceClient.SendToQueue<CommandResponse>(KioskBackOfficeConstants.kioskBackOfficeCommandQueueName, createOrganizationCommand);
        //        return Ok("sent");
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError($"Exception in OnBoardingController, CreateOrganization", exception);
        //        return StatusCode((int)HttpStatusCode.InternalServerError);
        //    }

        //}

    }
}
