using CBRegistration.DAL.Contexts;
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
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
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
                response.Message = "User created successfully";
                response.Data = user;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the user";
                response.Errors = [ex.Message];
            }

            return response;
        }

        public async Task<BaseResponseModel<bool>> ICNumberExistsAsync(int icNumber)
        {
            var response = new BaseResponseModel<bool>();

            try
            {
                response.Data = await _context.Users
                    .AnyAsync(u => u.ICNumber == icNumber && u.IsActive);

                response.Success = true;
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
