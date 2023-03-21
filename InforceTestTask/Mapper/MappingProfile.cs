using AutoMapper;
using InforceTestTask.Data.Entities;
using InforceTestTask.ViewModels;

namespace InforceTestTask.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShortUrlEntity, ShortUrlVM>();
        }
    }
}
