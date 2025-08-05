using Wavve.Core.Identity;

namespace Wavve.Core.Models
{
    public class Track
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string FileUrl { get; set; }
        public string? PreviewUrl { get; set; }
        public double Duration { get; set; }
        public DateTime UploadedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid? GenreId { get; set; }

        public ApplicationUser User { get; set; }
        public Genre? Genre { get; set; }
        public TrackStats? Stats { get; set; }
        public List<TrackTag> TrackTags { get; set; } = new();
        public List<TrackLike> Likes { get; set; } = new();
        public List<TrackComment> Comments { get; set; } = new();
        public List<PlaylistTrack> PlaylistTracks { get; set; } = new();
    }
}
