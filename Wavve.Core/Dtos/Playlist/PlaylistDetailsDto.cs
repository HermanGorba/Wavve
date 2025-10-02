using Wavve.Core.Dtos.Track;

namespace Wavve.Core.Dtos.Playlist;

public class PlaylistDetailsDto : PlaylistDto
{
    public DateTime CreatedAt { get; set; }
    public string? UserName { get; set; }
    public List<TrackDto> Tracks { get; set; } = new();
}