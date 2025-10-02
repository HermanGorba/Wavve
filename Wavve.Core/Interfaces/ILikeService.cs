namespace Wavve.Core.Interfaces;

public interface ILikeService
{
    Task LikeTrackAsync(int trackId, string? user, CancellationToken ct);
    Task UnlikeTrackAsync(int trackId, string? user, CancellationToken ct);
    Task LikePlaylistAsync(int playlistId, string? user, CancellationToken ct);
}