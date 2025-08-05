using Wavve.Core.Identity;

namespace Wavve.Core.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
        public List<PlaylistTrack> PlaylistTracks { get; set; } = new();
    }
}