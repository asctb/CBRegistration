using CBRegistration.BLL.Interfaces;
using CBRegistration.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.BLL.Repositories
{
    public class UserVerificationService : IUserVerificationService
    {
        public BaseResponseModel VerifyUserEmailByCode(int userId)
        {
            try
            {
                //Verify via users phone with SMTP server service 

                bool hasCodeMatched = true;

                if(!hasCodeMatched)
                {
                    return new BaseResponseModel<UserModel>
                    {
                        Success = false,
                        Message = $"User entered incorrect email OTP",
                    };
                }

                return new BaseResponseModel<UserModel>
                {
                    Success = true,
                    Message = $"User entered correct email OTP",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserModel>
                {
                    Success = false,
                    Message = "Error verifying user email OTP",
                    Errors = [ex.Message]
                };
            }
        }

        public BaseResponseModel VerifyUserPhoneByCode(int userId)
        {
            try
            {
                //Verify via users phone with SMTP server service 

                bool hasCodeMatched = true;

                if (!hasCodeMatched)
                {
                    return new BaseResponseModel<UserModel>
                    {
                        Success = false,
                        Message = $"User entered incorrect phone OTP",
                    };
                }

                return new BaseResponseModel<UserModel>
                {
                    Success = true,
                    Message = $"User entered correct phone OTP",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserModel>
                {
                    Success = false,
                    Message = "Error verifying user phone OTP",
                    Errors = [ex.Message]
                };
            }
        }
    }
}
