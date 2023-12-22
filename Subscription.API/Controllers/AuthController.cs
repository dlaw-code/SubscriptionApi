using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscription.API.Service.Interface;
using Subscription.DATA.Context;
using Subscription.MODEL.DTO;

namespace Subscription.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly SubscriptionContextDb _context;

        public AuthController(IAccountService accountService, SubscriptionContextDb context)
        {
            _accountService = accountService;
            _context = context;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUp signUp)
        {
            var registerUser = await _accountService.RegisterUser(signUp);
            if (registerUser.StatusCode == 200)
            {
                return Ok(registerUser);
            }
            else if (registerUser.StatusCode == 404)
            {
                return NotFound(registerUser);
            }
            else
            {
                return BadRequest(registerUser);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInModel signIn)
        {
            var loginUser = await _accountService.LoginUser(signIn);
            if (loginUser.StatusCode == 200)
             {
                return Ok(loginUser);
            }
            else if (loginUser.StatusCode == 404)
            {
                return NotFound(loginUser);
            }
            else
            {
                return BadRequest(loginUser);
            }
        }
    }
}
