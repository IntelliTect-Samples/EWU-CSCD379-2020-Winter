using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Gift, Gift>()
                .ForMember(property => property.Id, option => option.Ignore())
                .ForMember(property => property.CreatedBy, option => option.Ignore());
            CreateMap<User, User>()
                .ForMember(property => property.Id, option => option.Ignore())
                .ForMember(property => property.CreatedBy, option => option.Ignore()); ;
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
