using Wavve.Core.Dtos.Track;

namespace Wavve.Core.Interfaces;

public interface ITrackService
{
    Task<TrackDto> UploadAsync(Guid userId, TrackUploadDto dto);
    Task<TrackDetailsDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TrackDto>> GetAllAsync();
    Task<bool> UpdateAsync(Guid id, Guid userId, TrackUpdateDto dto);
    Task<bool> DeleteAsync(Guid id, Guid userId);
    Task IncrementListenCountAsync(Guid id);
    Task IncrementDownloadCountAsync(Guid id);
}