using Wavve.Core.Identity;

namespace Wavve.Core.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? TrackId { get; set; }
        public Guid? CommentId { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
        public Track? Track { get; set; }
        public TrackComment? Comment { get; set; }
    }
}