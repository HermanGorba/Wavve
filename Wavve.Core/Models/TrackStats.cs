namespace Wavve.Core.Models
{
    public class TrackStats
    {
        public Guid Id { get; set; }
        public Guid TrackId { get; set; }

        public int ListenCount { get; set; }
        public int DownloadCount { get; set; }

        public Track Track { get; set; }
    }
}
