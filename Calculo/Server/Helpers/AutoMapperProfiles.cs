using AutoMapper;
using Calculo.Shared.Entities;

namespace Calculo.Server.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BusinessEntity, BusinessEntity>()
                .ForMember(x => x.Logo, option => option.Ignore());
        }
    }
}
