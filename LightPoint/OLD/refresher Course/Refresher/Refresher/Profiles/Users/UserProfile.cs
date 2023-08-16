using AutoMapper;
using Refresher.Dtos.Users;
using Refresher.Models;

namespace Refresher.Profiles.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
