using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ArtistManagement.WebApi.Domain.Repositories
{
    internal class ArtistRepository : IArtistRepository
    {
        private readonly ArtistDbContext context;

        public ArtistRepository(ArtistDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<ArtistEntity> Get()
        {
            return context.Artists.AsQueryable();
        }
        public IQueryable<ArtistEntity> Get(Expression<Func<ArtistEntity, object>> includes)
        {
            return context.Artists.Include(includes).AsQueryable();
        }

        public ArtistEntity Get(string id)
        {
            return context.Artists.SingleOrDefault(e => e.Id.Equals(id));
        }

        public ArtistEntity Get(string id, Expression<Func<ArtistEntity, object>> includes)
        {
            return context.Artists.Include(includes).SingleOrDefault(e => e.Id.Equals(id));
        }

        #region Artist
            
        public void Add(ArtistEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(ArtistEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(ArtistEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        #endregion

        #region Track
            
        public void Add(TrackEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TrackEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TrackEntity entity)
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