using CBRegistration.Shared.Entities;
using CBRegistration.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<BaseResponseModel<UserEntity>> GetByIdAsync(int id);
        Task<BaseResponseModel<UserEntity>> GetByIcNumber(int icNumber);
        Task<BaseResponseModel<UserModel>> CreateUserAsync(UserModel user);
        Task<BaseResponseModel<UserEntity>> UpdateAsync(int userId, Action<UserEntity> updateAction);
        Task<BaseResponseModel<bool>> ICNumberExistsAsync(int icNumber);
        Task<BaseResponseModel<UserEntity>> SetUserPinAsync(int userId, string pin);
    }
}
