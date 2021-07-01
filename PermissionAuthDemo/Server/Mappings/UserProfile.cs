using AutoMapper;
using PermissionAuthDemo.Server.Data.Entities;
using PermissionAuthDemo.Shared.Responses.Identity;

namespace PermissionAuthDemo.Server.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, AppUser>().ReverseMap();
        }
    }
}