using AutoMapper;
using System.Reflection;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class AutomapperProfileConfiguration : Profile
    {
        public AutomapperProfileConfiguration()
        {
            CreateMap<User, User>().ForMember(property => property.Id, option => option.Ignore());
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
