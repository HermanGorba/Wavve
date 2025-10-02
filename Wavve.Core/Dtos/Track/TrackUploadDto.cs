namespace Wavve.Core.Dtos.Track;

public class TrackUploadDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string FileUrl { get; set; }
    public string? PreviewUrl { get; set; }
    public double Duration { get; set; }
    public Guid? GenreId { get; set; }
}