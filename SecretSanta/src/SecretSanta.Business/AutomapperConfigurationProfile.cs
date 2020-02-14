﻿using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {

            CreateMap<User, Dto.User>();
            CreateMap<User, Dto.UserInput>();
            CreateMap<Dto.UserInput, User>();
            //    SecretSanta.Data.Gift -> SecretSanta.Business.Dto.Gift
            CreateMap<Gift, Dto.Gift>();
            CreateMap<Dto.GiftInput, Gift>();
            CreateMap<Gift, Dto.GiftInput>();

            CreateMap<Group, Dto.Group>();
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
