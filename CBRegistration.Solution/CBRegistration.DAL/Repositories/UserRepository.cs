using AutoMapper;
using CBRegistration.DAL.Contexts;
using CBRegistration.DAL.Interfaces;
using CBRegistration.Shared.Entities;
using CBRegistration.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<UserEntity>> GetByIdAsync(int id)
        {
            var response = new BaseResponseModel<UserEntity>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

                response.Success = true;
                response.Data = user;
                response.Message = response.Data != null
                    ? "User retrieved successfully"
                    : "User not found";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error retrieving item";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<BaseResponseModel<UserModel>> GetByIcNumber(int icNumber)
        {
            var response = new BaseResponseModel<UserModel>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.ICNumber == icNumber && u.IsActive);

                response.Success = true;
                response.Data = _mapper.Map<UserModel>(user);
                response.Message = response.Data != null
                    ? "User retrieved successfully"
                    : "User not found";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error retrieving item";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<BaseResponseModel<UserModel>> CreateUserAsync(UserModel user)
        {
            var response = new BaseResponseModel<UserModel>();

            try
            {
                var userEntity = _mapper.Map<UserEntity>(user);

                userEntity.CreatedAt = DateTime.UtcNow;
                userEntity.IsActive = true;

                await _context.Users.AddAsync(userEntity);
                await _context.SaveChangesAsync();

                var userModel = _mapper.Map<UserModel>(userEntity);

                response.Success = true;
                response.Data = userModel;
                response.Message = "User created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the user";
                response.Errors = [ex.Message];
            }

            return response;
        }

        public async Task<BaseResponseModel<UserModel>> UpdateAsync(int userId, Action<UserEntity> updateAction)
        {
            var response = new BaseResponseModel<UserModel>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found or inactive";
                    return response;
                }

                updateAction(user);

                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<UserModel>(user);
                response.Success = true;
                response.Message = "User updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error updating user";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<BaseResponseModel<bool>> ICNumberExistsAsync(int icNumber)
        {
            var response = new BaseResponseModel<bool>();

            try
            {
                bool isExisting = await _context.Users
                   .AnyAsync(u => u.ICNumber == icNumber && u.IsActive);

                response.Success = true;
                response.Data = isExisting;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
