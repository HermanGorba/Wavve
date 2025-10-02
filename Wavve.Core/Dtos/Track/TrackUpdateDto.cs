namespace Wavve.Core.Dtos.Track;

public class TrackUpdateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid? GenreId { get; set; }
}