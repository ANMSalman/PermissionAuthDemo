using AutoMapper;
using PermissionAuthDemo.Server.Data.Entities;
using PermissionAuthDemo.Shared.Responses.Identity;

namespace PermissionAuthDemo.Server.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, AppRole>().ReverseMap();
        }
    }
}