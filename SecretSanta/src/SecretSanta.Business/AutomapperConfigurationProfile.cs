using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Data.Gift, Dto.Gift>();
            CreateMap<Dto.GiftInput, Data.Gift>();

            CreateMap<Data.Group, Dto.Group> ();
            CreateMap <Dto.GroupInput, Data.Group> ();

            CreateMap <Data.User, Dto.User> ();
            CreateMap <Dto.UserInput, Data.User> ();

            CreateMap<Data.Gift, Data.Gift>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<Data.User, Data.User>().ForMember(property => property.Id, option => option.Ignore());
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
