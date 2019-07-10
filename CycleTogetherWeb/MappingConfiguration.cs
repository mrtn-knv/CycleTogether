using AutoMapper;
using DAL.Models;
using WebModels;

namespace CycleTogetherWeb
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<User, UserEntry>();
            CreateMap<UserEntry, User>();
            CreateMap<Route, RouteEntry>();
            CreateMap<RouteEntry, Route>();
            CreateMap<EquipmentEntry, Equipment>();
            CreateMap<PictureEntry, Picture>();
            CreateMap<Picture, PictureEntry>();
            CreateMap<RouteEquipmentEntry, RouteEquipments>();
            CreateMap<UserRouteEntry, UserRoute>();
        }
    }
}
