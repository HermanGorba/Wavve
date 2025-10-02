namespace Wavve.Core.Dtos.Track;

public class TrackDetailsDto : TrackDto
{
    public int ListenCount { get; set; }
    public int DownloadCount { get; set; }
    public int Likes { get; set; }
    public int Comments { get; set; }
    public string? GenreName { get; set; }
    public string? UserName { get; set; }
}