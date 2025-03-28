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
        Task<BaseResponseModel<UserEntity>> SetUserPinAsync(int userId, string pin);
        Task<BaseResponseModel<UserEntity>> ConfirmUserPin(int userId, string pin);
        Task<BaseResponseModel<UserEntity>> LoginUserAsync(int icNumber);
        Task<BaseResponseModel<UserEntity>> UpdateBiometricLoginAsync(int userId, bool isEnabled);
        Task<BaseResponseModel<UserEntity>> AcceptTermsAndConditionsAsync(int userId);
    }
}
