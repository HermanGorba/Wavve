namespace Wavve.Core.Models
{
    public class TrackTag
    {
        public Guid Id { get; set; }
        public Guid TrackId { get; set; }
        public Guid TagId { get; set; }

        public Track Track { get; set; }
        public Tag Tag { get; set; }
    }
}