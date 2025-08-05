using Wavve.Core.Identity;

namespace Wavve.Core.Models
{
    public class ModerationAction
    {
        public Guid Id { get; set; }
        public Guid ModeratorId { get; set; }

        public string ActionType { get; set; } // e.g. BanUser, DeleteTrack
        public string TargetType { get; set; } // e.g. User, Track, Comment
        public Guid TargetId { get; set; }

        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser Moderator { get; set; }
    }
}