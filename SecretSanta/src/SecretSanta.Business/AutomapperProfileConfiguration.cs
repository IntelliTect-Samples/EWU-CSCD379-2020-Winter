using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class AutomapperProfileConfiguration : Profile
    {
        public AutomapperProfileConfiguration()
        {
            CreateMap<Gift, Gift>().ForMember(g => g.Id, option => option.Ignore());
            CreateMap<Group, Group>().ForMember(g => g.Id, option => option.Ignore());
            CreateMap<User, User>().ForMember(u => u.Id, option => option.Ignore());
        }

        static public IMapper CreateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
