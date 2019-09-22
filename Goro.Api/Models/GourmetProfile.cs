using AutoMapper;
using Goro.Api.Infrastructure.Models;

namespace Goro.Api.Models
{
    public class GourmetProfile : Profile
    {
        public GourmetProfile()
        {
            CreateMap<GourmetEntity, Gourmet>()
//                .IncludeBase<DocumentBase, Gourmet>()
                //.ForMember(d => d.Id, o => o.MapFrom(s => int.Parse(s.Id)))
                .ForMember(d => d.Closed, o => o.Condition(s => s.Closed != null))
                //.ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Location, o => o.MapFrom(s => s.ConvertLocation()));
                //.ForMember(d => d.Id, o => o.MapFrom(s => s.ConvertId()));;
        }
    }
}