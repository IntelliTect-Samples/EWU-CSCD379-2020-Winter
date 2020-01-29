using System.Reflection;
using AutoMapper;
using SecretSanta.Data;

// ReSharper disable IdentifierTypo - I know it should be AutoMapper, okay IDE?

namespace SecretSanta.Business
{

    public class AutomapperProfileConfiguration : Profile
    {

        public AutomapperProfileConfiguration()
        {
            CreateMap<User, User>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<Gift, Gift>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<Group, Group>().ForMember(property => property.Id, option => option.Ignore());
        }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddMaps(Assembly.GetExecutingAssembly()); });

            return mapperConfiguration.CreateMapper();
        }

    }

}
