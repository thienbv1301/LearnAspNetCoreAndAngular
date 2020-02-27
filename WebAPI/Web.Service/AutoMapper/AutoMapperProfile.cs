using AutoMapper;
using Web.Data.EntityModels;
using Web.Service.DtoModels;

namespace Web.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()    
                .ForMember(d=>d.Role,m=>m.MapFrom(s=>s.Role.Name));
        }
    }
}
