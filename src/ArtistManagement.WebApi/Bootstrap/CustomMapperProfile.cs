using System.Linq;
using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;

namespace ArtistManagement.WebApi.Bootstrap
{
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<AlbumEntity, AlbumModel>()
                .ForMember(m => m.Tracks, o => o.MapFrom(s => s.AlbumTracks.Select(e => e.Track)));

            CreateMap<TrackEntity, AlbumTrackModel>()
                .ForMember(m => m.Artist, o => o.MapFrom(s => s.Artist)) ;

            CreateMap<TrackEntity, TrackModel>()
                .ForMember(m => m.Albums, o => o.MapFrom(s => s.AlbumTracks.Select(e => e.Album)));

            CreateMap<AlbumEntity, TrackAlbumModel>()
                .ForMember(m => m.Release, o => o.MapFrom(s => s.Release.Date));
        }
    }
}