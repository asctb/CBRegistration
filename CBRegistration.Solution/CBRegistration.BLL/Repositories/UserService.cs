using CBRegistration.DAL.Interfaces;
using CBRegistration.Shared.Entities;
using CBRegistration.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.BLL.Repositories
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //CREATE USER MODEL, SERVICE SHOULDNT BE USING ENTITIES DIRECTLY
        public async Task<BaseResponseModel<UserEntity>> CreateUserAsync(UserEntity user)
        {
            var response = new BaseResponseModel<UserEntity>();

            var icNumberCheck = await _userRepository.IcNumberExistsAsync(user.ICNumber);
            if (!icNumberCheck.Success || icNumberCheck.Data)
            {
                response.Success = false;
                response.Message = icNumberCheck.Data
                    ? "IC Number already registered"
                    : "Error validating IC Number";
                response.Errors = icNumberCheck.Errors;
                return response;
            }

            // Rest of your validation and creation logic...
            try
            {
                var createResult = await _userRepository.CreateUserAsync(user);
                if (createResult.Success)
                {
                    response.Success = true;
                    response.Message = "User created successfully";
                    response.Data = createResult.Data;
                }
                else
                {
                    response = createResult;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error creating user";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
