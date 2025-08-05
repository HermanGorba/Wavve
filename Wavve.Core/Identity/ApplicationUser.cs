using Microsoft.AspNetCore.Identity;
using Wavve.Core.Models;

namespace Wavve.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Track> Tracks { get; set; } = new();
        public List<Playlist> Playlists { get; set; } = new();
        public List<UserFollow> Followers { get; set; } = new();
        public List<UserFollow> Followings { get; set; } = new();
        public List<TrackLike> Likes { get; set; } = new();
        public List<TrackComment> Comments { get; set; } = new();
    }
}