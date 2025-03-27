using CBRegistration.BLL.Helpers;
using CBRegistration.BLL.Interfaces;
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
    public class UserService : IUserService
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

            var icNumberCheck = await _userRepository.ICNumberExistsAsync(user.ICNumber);
            if (!icNumberCheck.Success || icNumberCheck.Data)
            {
                response.Success = false;
                response.Message = icNumberCheck.Data
                    ? "IC Number already registered"
                    : "Error validating IC Number";
                response.Errors = icNumberCheck.Errors;
                return response;
            }

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

        public async Task<BaseResponseModel<UserEntity>> SetUserPinAsync(int userId, string pin)
        {
            var response = new BaseResponseModel<UserEntity>();
            int intPin = Convert.ToInt32(pin);

            var userResponse = await _userRepository.GetByIdAsync(userId);
            if (!userResponse.Success || userResponse.Data == null)
            {
                response.Success = false;
                response.Message = "User not found or inactive";
                return response;
            }

            if (!userResponse.Data.HasAcceptedTermsConditions)
            {
                response.Success = false;
                response.Message = "User did not accepted Terms and Conditions";
                return response;
            }

            // Additional pin verification if needed
            if (intPin < 99999 || intPin > 999999)
            {
                response.Success = false;
                response.Message = "PIN must be exactly 6 digits";
                return response;
            }

            var securedPin = PinSecurityHelper.HashPin(pin.ToString());

            return await _userRepository.SetUserPinAsync(userId, securedPin);
        }
    }
}
