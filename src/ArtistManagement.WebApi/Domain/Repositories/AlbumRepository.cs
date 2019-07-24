using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistManagement.WebApi.Domain.Repositories
{
    internal class AlbumRepository : IAlbumRepository
    {
        private readonly ArtistDbContext context;

        public AlbumRepository(ArtistDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<AlbumEntity> Get()
        {
            return context.Albums
                .Include(e => e.AlbumTracks)
                    .ThenInclude(e => e.Track)
                    .ThenInclude(e => e.Artist)
                .AsQueryable();
        }
        public IQueryable<AlbumEntity> Get(Expression<Func<AlbumEntity, object>> includes)
        {
            return context.Albums.Include(includes).AsQueryable();
        }

        public AlbumEntity Get(string id)
        {
            return context.Albums
                .Include(e => e.AlbumTracks)
                    .ThenInclude(e => e.Track)
                    .ThenInclude(e => e.Artist)
                .SingleOrDefault(e => e.Id.Equals(id));
        }

        public AlbumEntity Get(string id, Expression<Func<AlbumEntity, object>> includes)
        {
            return context.Albums.Include(includes).SingleOrDefault(e => e.Id.Equals(id));
        }

        #region Album
            
        public void Add(AlbumEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(AlbumEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(AlbumEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        #endregion

        #region Album Track
            
        public void Add(AlbumTrackEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(AlbumTrackEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(AlbumTrackEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }
        
        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}