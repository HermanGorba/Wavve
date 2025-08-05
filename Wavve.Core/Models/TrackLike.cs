using Wavve.Core.Identity;

namespace Wavve.Core.Models
{
    public class TrackLike
    {
        public Guid Id { get; set; }
        public Guid TrackId { get; set; }
        public Guid UserId { get; set; }

        public Track Track { get; set; }
        public ApplicationUser User { get; set; }
    }
}