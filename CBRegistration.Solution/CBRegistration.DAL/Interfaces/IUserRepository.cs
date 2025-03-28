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
        Task<BaseResponseModel<UserModel>> GetByIcNumber(int icNumber);
        Task<BaseResponseModel<UserModel>> CreateUserAsync(UserModel user);
        Task<BaseResponseModel<UserModel>> UpdateAsync(int userId, Action<UserEntity> updateAction);
        Task<BaseResponseModel<bool>> ICNumberExistsAsync(int icNumber);
    }
}
