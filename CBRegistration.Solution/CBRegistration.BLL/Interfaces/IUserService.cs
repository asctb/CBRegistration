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
        Task<BaseResponseModel<UserModel>> UpdateUserPinAsync(int userId, string pin);
        Task<BaseResponseModel<PinModel>> ConfirmUserPinAsync(int userId, string pin);
        Task<BaseResponseModel<UserModel>> LoginUserAsync(int icNumber);
        Task<BaseResponseModel<UserModel>> SetPhoneVerified(int userId);
        Task<BaseResponseModel<UserModel>> SetEmailVerified(int userId);
        Task<BaseResponseModel<UserModel>> SetBiometricLoginAsync(int userId, bool isEnabled);
        Task<BaseResponseModel<UserModel>> SetTermsAndConditionsAsync(int userId, bool isAccepted);
    }
}
