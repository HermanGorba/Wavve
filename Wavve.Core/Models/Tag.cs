namespace Wavve.Core.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<TrackTag> TrackTags { get; set; } = new();
    }
}