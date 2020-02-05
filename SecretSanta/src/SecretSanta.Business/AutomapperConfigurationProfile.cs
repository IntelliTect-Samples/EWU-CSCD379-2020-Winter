﻿using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            //Add Automappers for Dtos
            CreateMap<Gift, Dto.Gift>();
            CreateMap<Group, Dto.Group>();
            CreateMap<User, Dto.User>();
            CreateMap<Dto.GiftInput, Gift>();
            CreateMap<Dto.UserInput, User>();
            CreateMap<Dto.GroupInput, Group>();

            CreateMap<Gift, Gift>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<User, User>().ForMember(property => property.Id, option => option.Ignore());
        }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
