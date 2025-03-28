using CBRegistration.Shared.Entities;
using CBRegistration.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.BLL.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponseModel<UserModel>> CreateUserAsync(CreateUserModel user);
        Task<BaseResponseModel<UserModel>> SetUserPinAsync(int userId, string pin);
        Task<BaseResponseModel<PinModel>> ConfirmUserPinAsync(int userId, string pin);
        Task<BaseResponseModel<UserModel>> LoginUserAsync(int icNumber);
        Task<BaseResponseModel<UserModel>> SetPhoneVerified(int userId, bool isVerified);
        Task<BaseResponseModel<UserModel>> SetEmailVerified(int userId, bool isVerified);
        Task<BaseResponseModel<UserModel>> UpdateBiometricLoginAsync(int userId, bool isEnabled);
        Task<BaseResponseModel<UserModel>> AcceptTermsAndConditionsAsync(int userId);
    }
}
