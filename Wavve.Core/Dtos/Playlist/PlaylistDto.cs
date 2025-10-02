namespace Wavve.Core.Dtos.Playlist;

public class PlaylistDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public int TrackCount { get; set; }
}