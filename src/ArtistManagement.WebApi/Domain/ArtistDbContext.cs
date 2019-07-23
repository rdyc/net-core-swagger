using ArtistManagement.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtistManagement.WebApi.Domain
{
    internal class ArtistDbContext : DbContext
    {
        public ArtistDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ArtistEntity> Artists { get; set; }
        public DbSet<TrackEntity> Tracks { get; set; }
        public DbSet<AlbumEntity> Albums { get; set; }
        public DbSet<AlbumTrackEntity> AlbumTracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistEntity>(ConfigureArtist);
            modelBuilder.Entity<TrackEntity>(ConfigureTrack);
            modelBuilder.Entity<AlbumEntity>(ConfigureAlbum);
            modelBuilder.Entity<AlbumTrackEntity>(ConfigureAlbumTrack);
        }

        private static void ConfigureArtist(EntityTypeBuilder<ArtistEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Tracks).WithOne(e => e.Artist).HasForeignKey(e => e.ArtistId);
            builder.HasMany(e => e.Albums).WithOne(e => e.Artist).HasForeignKey(e => e.ArtistId);

            builder.Property(e => e.Id).HasMaxLength(14).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Nationality).HasMaxLength(1).IsRequired();
        }

        private static void ConfigureTrack(EntityTypeBuilder<TrackEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Artist).WithMany(e => e.Tracks).HasForeignKey(e => e.ArtistId);

            builder.Property(e => e.Id).HasMaxLength(14).IsRequired();
            builder.Property(e => e.ArtistId).HasMaxLength(14).IsRequired();
            builder.Property(e => e.Title).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Genre).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Duration).IsRequired();
        }

        private static void ConfigureAlbum(EntityTypeBuilder<AlbumEntity> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.Artist).WithMany(e => e.Albums).HasForeignKey(e => e.ArtistId);
            builder.HasMany(e => e.Tracks).WithOne(e => e.Album).HasForeignKey(e => e.AlbumId);

            builder.Property(e => e.Id).HasMaxLength(14).IsRequired();
            builder.Property(e => e.ArtistId).HasMaxLength(14).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Release).IsRequired();
        }

        private static void ConfigureAlbumTrack(EntityTypeBuilder<AlbumTrackEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Album).WithMany(e => e.Tracks).HasForeignKey(e => e.AlbumId);
            builder.HasOne(e => e.Track).WithMany(e => e.AlbumTracks).HasForeignKey(e => e.TrackId);

            builder.Property(e => e.Id).HasMaxLength(14).IsRequired();
            builder.Property(e => e.AlbumId).HasMaxLength(14).IsRequired();
            builder.Property(e => e.TrackId).HasMaxLength(14).IsRequired();
        }
    }
}