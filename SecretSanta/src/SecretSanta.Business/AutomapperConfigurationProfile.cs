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

            CreateMap<Gift, Dto.Gift>();
            CreateMap<User, Dto.User>();
            CreateMap<Group, Dto.Group>();
            CreateMap<Dto.GiftInput, Gift>();
            CreateMap<Dto.UserInput, User>();
            CreateMap<Dto.GroupInput, Group>();
            CreateMap<Gift, Dto.GiftInput>();
            CreateMap<User, Dto.UserInput>();
            CreateMap<Group, Dto.GroupInput>();
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
