using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Infrastructure;
using AutoMapper;

namespace ArtistManagement.WebApi.Bootstrap
{
    public class MapperProfile<E, M> : Profile where E : Entity where M : BaseModel
    {
        public MapperProfile()
        {
            CreateMap<E, M>();
            
            CreateMap<E, SingleResponse<M>>()
                .ForMember(m => m.Data, o => o.MapFrom(s => s));
            
            CreateMap<PageDto, Page>();

            CreateMap<CollectionDto<E>, Meta>()
                .ForMember(m => m.Size, o => o.MapFrom(s => s.Size))
                .ForMember(m => m.Total, o => o.MapFrom(s => s.Total))
                .ForMember(m => m.Page, o => o.MapFrom(s => s.Page));

            CreateMap<CollectionDto<E>, ListResponse<M>>()
                .ForMember(m => m.Data, o => o.MapFrom(s => s.Data));

            CreateMap<CollectionDto<E>, CollectionResponse<M>>()
                .ForMember(m => m.Meta, o => o.MapFrom(s => s));
        }
    }
}