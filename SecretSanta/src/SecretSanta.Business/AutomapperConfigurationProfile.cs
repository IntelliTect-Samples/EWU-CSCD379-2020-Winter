using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Gift, Dto.Gift>();
            CreateMap<Gift, Dto.GiftInput>();
            CreateMap<Dto.GiftInput, Gift>();

            CreateMap<User, Dto.User>();
            CreateMap<User, Dto.UserInput>();
            CreateMap<Dto.UserInput, User>();

            CreateMap<Group, Dto.Group>();
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
