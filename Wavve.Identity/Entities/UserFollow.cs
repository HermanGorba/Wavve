namespace Wavve.Identity.Entities
{
    public class UserFollow
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }

        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}
