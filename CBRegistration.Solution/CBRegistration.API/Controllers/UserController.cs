using CBRegistration.BLL.Interfaces;
using CBRegistration.Shared.Entities;
using CBRegistration.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBRegistration.API.Controllers
{
    //POSTMANDAN OLUCAK ŞEKİLDE ROUTE GÜNCELLE
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponseModel
                {
                    Success = false,
                    Message = "Invalid user data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            var result = await _userService.CreateUserAsync(user);

            //ŞURASI BELKİ NOT FOUND DÖNEBİLİR
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
