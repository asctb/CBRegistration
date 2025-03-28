using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<UserModel>> CreateUserAsync(CreateUserModel user)
        {
            var response = new BaseResponseModel<UserModel>();

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
                var userModel = _mapper.Map<UserModel>(user);
                var createResult = await _userRepository.CreateUserAsync(userModel);
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

            return await _userRepository.UpdateAsync(userId, user =>
            {
                user.Pin = securedPin;
            });
        }

        public async Task<BaseResponseModel<UserEntity>> ConfirmUserPin(int userId, string pin)
        {
            var response = new BaseResponseModel<UserEntity>();
            string securedPin = PinSecurityHelper.HashPin(pin.ToString());

            var userResponse = await _userRepository.GetByIdAsync(userId);
            if (!userResponse.Success || userResponse.Data == null)
            {
                response.Success = false;
                response.Message = "User not found or inactive";
                return response;
            }

            bool hasPinMatched = PinSecurityHelper.VerifyPin(pin, userResponse.Data.Pin);

            if (!hasPinMatched)
            {
                response.Success = false;
                response.Message = "User pin is unmatched.";
                return response;
            }

            response.Success = true;
            response.Message = "User pin is matched.";
            return response;
        }

        public async Task<BaseResponseModel<UserEntity>> LoginUserAsync(int icNumber)
        {
            var response = new BaseResponseModel<UserEntity>();

            var userResponse = await _userRepository.GetByIcNumber(icNumber);
            if (!userResponse.Success || userResponse.Data == null)
            {
                response.Success = false;
                response.Message = "User not found or inactive";
                return response;
            }

            response.Success = true;
            response.Message = "User retrieved successfully";
            response.Data = userResponse.Data;
            return response;
        }

        public async Task<BaseResponseModel<UserEntity>> UpdateBiometricLoginAsync(int userId, bool isEnabled)
        {
            try
            {
                var result = await _userRepository.UpdateAsync(userId, user =>
                {
                    user.IsBiometricLoginEnabled = isEnabled;
                });

                if (!result.Success)
                {
                    return new BaseResponseModel<UserEntity>
                    {
                        Success = false,
                        Message = result.Message,
                        Errors = result.Errors
                    };
                }

                return new BaseResponseModel<UserEntity>
                {
                    Success = true,
                    Message = $"Biometric login {(isEnabled ? "enabled" : "disabled")} successfully",
                    Data = result.Data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserEntity>
                {
                    Success = false,
                    Message = "Error updating biometric login status",
                    Errors = [ex.Message]
                };
            }
        }

        public async Task<BaseResponseModel<UserEntity>> AcceptTermsAndConditionsAsync(int userId)
        {
            try
            {
                var result = await _userRepository.UpdateAsync(userId, user =>
                {
                    user.HasAcceptedTermsConditions = true;
                });

                if (!result.Success)
                {
                    return new BaseResponseModel<UserEntity>
                    {
                        Success = false,
                        Message = result.Message,
                        Errors = result.Errors
                    };
                }

                return new BaseResponseModel<UserEntity>
                {
                    Success = true,
                    Message = "Terms and conditions accepted successfully",
                    Data = result.Data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserEntity>
                {
                    Success = false,
                    Message = "Error accepting terms and conditions",
                    Errors = new List<string> { ex.Message }
                };
            }
        }       
    }
}
