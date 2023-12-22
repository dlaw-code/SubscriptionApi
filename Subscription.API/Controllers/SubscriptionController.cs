using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subscription.API.Service.Interface;
using Subscription.MODEL.DTO;

namespace Subscription.API.Controllers
{
    [Route("api/subscription")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscribeDto subscribe)
        {
            var response = await _subscriptionService.SubscribeAsync(subscribe);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpPost("unsubcribe")]
        public async Task<IActionResult> UnSubscribe(SubscribeDto unsubscribe)
        {
            var response = await _subscriptionService.UnSubscribeAsync(unsubscribe);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpPost("status")]
        public async Task<IActionResult> Status(SubscribeDto status)
        {
            var response = await _subscriptionService.SubscribeStatusAsync(status);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
