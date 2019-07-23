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

namespace ArtistManagement.WebApi.Application.Services
{
    internal class ArtistService : IArtistService
    {
        private readonly IArtistRepository repository;
        private readonly IMapper mapper;

        public ArtistService(IArtistRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CollectionResponse<ArtistModel>> Get(QueryStringRequest<ArtistField> request)
        {
            try
            {
                // get all
                var data = repository.Get();

                // filter find
                if(!string.IsNullOrEmpty(request.Find) && !request.FindBy.HasValue)
                {
                    data = data.Where(e => 
                        e.Id.Contains(request.Find) || 
                        e.Name.Contains(request.Find) || 
                        e.Nationality.Contains(request.Find));
                }

                // filter find & find by
                if(!string.IsNullOrEmpty(request.Find) && request.FindBy.HasValue)
                {
                    switch (request.FindBy.Value)
                    {
                        case ArtistField.Id :
                            data = data.Where(e => e.Id.Contains(request.Find));
                            break;

                        case ArtistField.Name :
                            data = data.Where(e => e.Name.Contains(request.Find));
                            break;

                        case ArtistField.Nationality :
                            data = data.Where(e => e.Nationality.Contains(request.Find));
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
                        case ArtistField.Id :
                            data = data.OrderBy(e => e.Id);
                            break;

                        case ArtistField.Name :
                            data = data.OrderBy(e => e.Name);
                            break;

                        case ArtistField.Nationality :
                            data = data.OrderBy(e => e.Nationality);
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
                        case ArtistField.Id :
                            data = request.SortDirection.Equals(SortDirection.Ascending) ? 
                                data.OrderBy(e => e.Id) : 
                                data.OrderByDescending(e => e.Id);
                            break;

                        case ArtistField.Name :
                            data = request.SortDirection.Equals(SortDirection.Ascending) ? 
                                data.OrderBy(e => e.Name) : 
                                data.OrderByDescending(e => e.Name);
                            break;

                        case ArtistField.Nationality :
                            data = request.SortDirection.Equals(SortDirection.Ascending) ? 
                                data.OrderBy(e => e.Nationality) : 
                                data.OrderByDescending(e => e.Nationality);
                            break;

                        default:
                            break;
                    }
                }

                // parse into data transfer object
                var dto = new CollectionDto<ArtistEntity>
                {
                    Total = data.Count(),
                    Size = request.Size,
                    Data = data
                        .Skip(request.Page * request.Size)
                        .Take(request.Size)
                        .AsEnumerable()
                };

                // mapping into result
                var result = mapper.Map<CollectionResponse<ArtistModel>>(dto.WithPage(request.Page));

                return await Task.FromResult(result);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleResponse<ArtistModel>> Get(string id)
        {
            try
            {
                // get by id
                var data = repository.Get(id);

                // mapping into result
                var result = mapper.Map<SingleResponse<ArtistModel>>(data);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleResponse<ArtistModel>> Add(ArtistPayloadModel payload)
        {
            try
            {
                // create new
                var data = new ArtistEntity(payload.Name, payload.Nationality);

                // add into repository
                repository.Add(data);

                // save changes
                await repository.SaveChangesAsync();

                // mapping into result
                return mapper.Map<SingleResponse<ArtistModel>>(data);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleResponse<ArtistModel>> Update(ArtistPayloadModel payload)
        {
            try
            {
                // get by id
                var data = repository.Get(payload.Id);
                
                // set update
                data.SetUpdate(payload.Name, payload.Nationality);

                // update repository
                repository.Update(data);

                // save changes
                await repository.SaveChangesAsync();

                // mapping into result
                return mapper.Map<SingleResponse<ArtistModel>>(data);   
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
                // get by id
                var data = repository.Get(id);

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
    }
}