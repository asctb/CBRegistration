using CBRegistration.BLL.Interfaces;
using CBRegistration.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SetUserPin([FromBody] SetPinRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponseModel
                {
                    Success = false,
                    Message = "Invalid request",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                        .ToList()
                });
            }

            var result = await _userService.SetUserPinAsync(request.UserId, request.Pin.ToString());

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUserPin([FromBody] SetPinRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponseModel
                {
                    Success = false,
                    Message = "Invalid request",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                        .ToList()
                });
            }

            var result = await _userService.ConfirmUserPin(request.UserId, request.Pin.ToString());

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
