using AutoMapper;
using BlogEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlogEngine.Business
{
    public class AutomapperProfileConfiguration : Profile
    {
        public AutomapperProfileConfiguration()
        {
            CreateMap<Author, Author>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<Tag, Tag>().ForMember(property => property.Id, option => option.Ignore());
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
