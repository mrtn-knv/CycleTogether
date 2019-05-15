using AutoMapper;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebModels;

namespace CycleTogetherWeb
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<UserWeb, User>();
            CreateMap<User, UserWeb>();
        }
    }
}
