using System;
using System.Linq;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Fields;
using ArtistManagement.WebApi.Application.Requests;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.Domain.Repositories;
using ArtistManagement.WebApi.Infrastructure;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArtistManagement.WebApi.Application.Services
{
    internal class TrackService : ITrackService
    {
        private readonly IArtistRepository repository;
        private readonly IMapper mapper;

        public TrackService(IArtistRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CollectionResponse<TrackModel>> Get(QueryRequest<TrackField> request)
        {
            try
            {
                // get all
                var artist = repository.Get(e => e.Tracks);
                var data = artist.SelectMany(e => e.Tracks);
                
                // include relation
                data = data
                    .Include(e => e.Artist)
                    .Include(e => e.AlbumTracks)
                        .ThenInclude(e => e.Album);

                // filter find
                if(!string.IsNullOrEmpty(request.Find) && !request.FindBy.HasValue)
                {
                    data = data.Where(e => 
                        e.Id.Contains(request.Find) || 
                        e.Title.Contains(request.Find));
                }

                // filter find & find by
                if(!string.IsNullOrEmpty(request.Find) && request.FindBy.HasValue)
                {
                    switch (request.FindBy.Value)
                    {
                        case TrackField.Id :
                            data = data.Where(e => e.Id.Contains(request.Find));
                            break;

                        case TrackField.Title :
                            data = data.Where(e => e.Title.Contains(request.Find));
                            break;

                        default:
                            break;
                    }
                }

                // sorting by field
                if(request.SortBy.HasValue && !request.SortDirection.HasValue)
                {
                    switch (request.SortBy.Value)
                    {
                        case TrackField.Id :
                            data = data.OrderBy(e => e.Id);
                            break;

                        case TrackField.Title :
                            data = data.OrderBy(e => e.Title);
                            break;

                        default:
                            break;
                    }
                }

                // sorting by field & direction
                if(request.SortBy.HasValue && request.SortDirection.HasValue)
                {
                    switch (request.SortBy.Value)
                    {
                        case TrackField.Id :
                            data = request.SortDirection.Equals(SortDirection.Ascending) ? 
                                data.OrderBy(e => e.Id) : 
                                data.OrderByDescending(e => e.Id);
                            break;

                        case TrackField.Title :
                            data = request.SortDirection.Equals(SortDirection.Ascending) ? 
                                data.OrderBy(e => e.Title) : 
                                data.OrderByDescending(e => e.Title);
                            break;

                        default:
                            break;
                    }
                }

                // parse into data transfer object
                var dto = new CollectionDto<TrackEntity>
                {
                    Total = data.Count(),
                    Size = request.Size,
                    Data = data
                        .Skip(request.Page * request.Size)
                        .Take(request.Size)
                        .AsEnumerable()
                };

                // mapping into result
                var result = mapper.Map<CollectionResponse<TrackModel>>(dto.WithPage(request.Page));

                return await Task.FromResult(result);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleResponse<TrackModel>> Get(string id)
        {
            try
            {
                // get artist by id
                var artist = repository.Get(e => e.Tracks);
                
                // get track by id
                var data = artist.SelectMany(e => e.Tracks.Where(t => t.Id.Equals(id)))
                    .Include(e => e.Artist)
                    .Include(e => e.AlbumTracks)
                        .ThenInclude(e => e.Album)
                    .SingleOrDefault();

                // mapping into result
                var result = mapper.Map<SingleResponse<TrackModel>>(data);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleResponse<TrackModel>> Add(TrackPostModel payload)
        {
            try
            {
                // get artist by id
                var artist = repository.Get(payload.ArtistId, e => e.Tracks);

                // add new track
                var data = artist.AddTrack(payload.Title, payload.Genre, payload.Duration);

                // add into repository
                repository.Add(data);

                // save changes
                await repository.SaveChangesAsync();

                // mapping into result
                return mapper.Map<SingleResponse<TrackModel>>(data);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleResponse<TrackModel>> Update(TrackPutModel payload)
        {
            try
            {
                // get artist by id
                var artist = repository.Get(payload.ArtistId, e => e.Tracks);
                
                // get track by id
                var data = artist.Tracks.SingleOrDefault(e => e.Id.Equals(payload.Id));
                
                // set udate
                data.SetUpdate(payload.Title, payload.Genre, payload.Duration);

                // update repository
                repository.Update(data);

                // save changes
                await repository.SaveChangesAsync();

                // mapping into result
                return mapper.Map<SingleResponse<TrackModel>>(data);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                // get all
                var artist = repository.Get(e => e.Tracks);

                // get track by id
                var data = artist.SelectMany(e => e.Tracks.Where(t => t.Id.Equals(id))).SingleOrDefault();

                // delete from repository
                repository.Delete(data);

                // save changes
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsValidAsync(string id)
        {
            try
            {
                // get all
                var data = repository.Get(e => e.Tracks);

                var result = data.SelectMany(e => e.Tracks)
                    .Any(e => e.Id.Equals(id));
                
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}