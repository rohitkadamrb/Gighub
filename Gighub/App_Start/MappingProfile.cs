using AutoMapper;
using Gighub.Core.Dtos;
using Gighub.Core.Models;

namespace Gighub.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            Mapper.CreateMap<Gig, GigDto>();

            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }

    }
}