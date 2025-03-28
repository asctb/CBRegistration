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

        public UserRepository(AppDbContext context)
        {
            _context = context;
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

        public async Task<BaseResponseModel<UserEntity>> GetByIcNumber(int icNumber)
        {
            var response = new BaseResponseModel<UserEntity>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.ICNumber == icNumber && u.IsActive);

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

        public async Task<BaseResponseModel<UserEntity>> CreateUserAsync(UserEntity user)
        {
            var response = new BaseResponseModel<UserEntity>();

            try
            {
                user.CreatedAt = DateTime.UtcNow;
                user.IsActive = true;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = user;
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

        public async Task<BaseResponseModel<UserEntity>> UpdateAsync(int userId, Action<UserEntity> updateAction)
        {
            var response = new BaseResponseModel<UserEntity>();

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

                response.Data = user;
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

        public async Task<BaseResponseModel<UserEntity>> SetUserPinAsync(int userId, string pin)
        {
            var response = new BaseResponseModel<UserEntity>();

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

                user.Pin = pin;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "PIN updated successfully";
                response.Data = user;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error updating PIN";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
