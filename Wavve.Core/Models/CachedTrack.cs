namespace Wavve.Core.Models
{
    public class CachedTrack
    {
        public Guid Id { get; set; }
        public Guid TrackId { get; set; }
        public DateTime CachedAt { get; set; }

        public Track Track { get; set; }
    }
}