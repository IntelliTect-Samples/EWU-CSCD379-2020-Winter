﻿using AutoMapper;
using System.Reflection;
using SecretSanta.Data;

namespace BlogEngine.Business
{
    public class AutomapperProfileConfiguration : Profile
    {
        public AutomapperProfileConfiguration()
        {
            CreateMap<Gift, Gift>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<User, User>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<Group, Group>().ForMember(property => property.Id, option => option.Ignore());
        }

        static public IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
