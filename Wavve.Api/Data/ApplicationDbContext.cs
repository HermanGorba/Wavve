using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wavve.Core.Identity;
using Wavve.Core.Models;

namespace Wavve.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackStats> TrackStats { get; set; }
        public DbSet<TrackLike> TrackLikes { get; set; }
        public DbSet<TrackComment> TrackComments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TrackTag> TrackTags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        // ?? public DbSet<CachedTrack> CachedTracks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ModerationAction> ModerationActions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Followee)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Track>()
                .HasOne(t => t.Stats)
                .WithOne(s => s.Track)
                .HasForeignKey<TrackStats>(s => s.TrackId);

            modelBuilder.Entity<TrackTag>()
                .HasIndex(tt => new { tt.TrackId, tt.TagId }).IsUnique();

            modelBuilder.Entity<PlaylistTrack>()
                .HasIndex(pt => new { pt.PlaylistId, pt.TrackId }).IsUnique();

            modelBuilder.Entity<TrackLike>()
                .HasIndex(tl => new { tl.TrackId, tl.UserId }).IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email).IsUnique();
        }
    }

}