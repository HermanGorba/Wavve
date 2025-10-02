using Wavve.Core.Dtos.Playlist;

namespace Wavve.Core.Interfaces;

public interface IPlaylistService
{
    Task<PlaylistDto> CreateAsync(Guid userId, PlaylistCreateDto dto);
    Task<PlaylistDetailsDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<PlaylistDto>> GetAllAsync();
    Task<bool> UpdateAsync(Guid id, Guid userId, PlaylistUpdateDto dto);
    Task<bool> DeleteAsync(Guid id, Guid userId);
    Task<bool> AddTrackAsync(Guid playlistId, Guid userId, PlaylistAddTrackDto dto);
    Task<bool> RemoveTrackAsync(Guid playlistId, Guid userId, Guid trackId);
}