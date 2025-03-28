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
        public async Task<IActionResult> UpdateUserPinAsync([FromBody] ConfirmPinRequestModel request)
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

            var result = await _userService.UpdateUserPinAsync(request.UserId, request.Pin.ToString());

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUserPinAsync([FromBody] ConfirmPinRequestModel request)
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

            var result = await _userService.ConfirmUserPinAsync(request.UserId, request.Pin.ToString());

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequestModel request)
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

            var result = await _userService.LoginUserAsync(request.ICNumber);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetPhoneVerified([FromBody] SetPhoneVerifiedRequestModel request)
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

            var result = await _userService.SetPhoneVerified(request.UserId, request.IsVerified);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetEmailVerified([FromBody] SetEmailVerifiedRequestModel request)
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

            var result = await _userService.SetEmailVerified(request.UserId, request.IsVerified);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetBiometricLogin([FromBody] SetBiometricRequestModel request)
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

            var result = await _userService.SetBiometricLoginAsync(request.UserId, request.IsEnabled);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetTermsAndConditionsAsync([FromBody] SetTermsAndCondsRequestModel request)
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

            var result = await _userService.SetTermsAndConditionsAsync(request.UserId, request.IsAccepted);
            return Ok(result);
        }

    }
}
