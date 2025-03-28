using AutoMapper;
using CBRegistration.Shared.Entities;
using CBRegistration.Shared.Models;

namespace CBRegistration.API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserAsyncModel, CreateUserModel>();
            CreateMap<CreateUserModel, UserModel>();
            CreateMap<UserEntity, PinModel>();
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}
