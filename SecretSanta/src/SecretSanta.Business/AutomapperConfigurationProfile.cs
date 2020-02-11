using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Gift, Gift>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<User, User>().ForMember(property => property.Id, option => option.Ignore());

            CreateMap<User, Dto.User>();
            CreateMap<Gift, Dto.Gift>();
            CreateMap<Group, Dto.Group>();

            CreateMap<Dto.UserInput, User>();
            CreateMap<Dto.GiftInput, Gift>();
            CreateMap<Dto.GroupInput, Group>();

            CreateMap<Dto.UserInput, Dto.User>();
            CreateMap<Dto.GroupInput, Dto.Group>();
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
