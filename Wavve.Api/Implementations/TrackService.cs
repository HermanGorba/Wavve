using Microsoft.EntityFrameworkCore;
using Wavve.Api.Data;
using Wavve.Core.Dtos.Track;
using Wavve.Core.Interfaces;
using Wavve.Core.Models;

namespace Wavve.Api.Implementations;

public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext _db;

        public TrackService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TrackDto> UploadAsync(Guid userId, TrackUploadDto dto)
        {
            var track = new Track
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                PreviewUrl = dto.PreviewUrl,
                Duration = dto.Duration,
                GenreId = dto.GenreId,
                UploadedAt = DateTime.UtcNow,
                UserId = userId,
                Stats = new TrackStats
                {
                    Id = Guid.NewGuid(),
                    ListenCount = 0,
                    DownloadCount = 0
                }
            };

            _db.Tracks.Add(track);
            await _db.SaveChangesAsync();

            return new TrackDto
            {
                Id = track.Id,
                Title = track.Title,
                Description = track.Description,
                FileUrl = track.FileUrl,
                PreviewUrl = track.PreviewUrl,
                Duration = track.Duration,
                UploadedAt = track.UploadedAt,
                UserId = track.UserId,
                GenreId = track.GenreId
            };
        }

        public async Task<TrackDetailsDto?> GetByIdAsync(Guid id)
        {
            var track = await _db.Tracks
                .Include(t => t.Stats)
                .Include(t => t.User)
                .Include(t => t.Genre)
                .Include(t => t.Likes)
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (track == null) return null;

            return new TrackDetailsDto
            {
                Id = track.Id,
                Title = track.Title,
                Description = track.Description,
                FileUrl = track.FileUrl,
                PreviewUrl = track.PreviewUrl,
                Duration = track.Duration,
                UploadedAt = track.UploadedAt,
                UserId = track.UserId,
                GenreId = track.GenreId,
                ListenCount = track.Stats?.ListenCount ?? 0,
                DownloadCount = track.Stats?.DownloadCount ?? 0,
                Likes = track.Likes.Count,
                Comments = track.Comments.Count,
                GenreName = track.Genre?.Name,
                UserName = track.User?.UserName
            };
        }

        public async Task<IEnumerable<TrackDto>> GetAllAsync()
        {
            return await _db.Tracks
                .OrderByDescending(t => t.UploadedAt)
                .Select(t => new TrackDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    FileUrl = t.FileUrl,
                    PreviewUrl = t.PreviewUrl,
                    Duration = t.Duration,
                    UploadedAt = t.UploadedAt,
                    UserId = t.UserId,
                    GenreId = t.GenreId
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, Guid userId, TrackUpdateDto dto)
        {
            var track = await _db.Tracks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (track == null) return false;

            track.Title = dto.Title;
            track.Description = dto.Description;
            track.GenreId = dto.GenreId;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var track = await _db.Tracks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (track == null) return false;

            _db.Tracks.Remove(track);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task IncrementListenCountAsync(Guid id)
        {
            var stats = await _db.TrackStats.FirstOrDefaultAsync(s => s.TrackId == id);
            if (stats != null)
            {
                stats.ListenCount++;
                await _db.SaveChangesAsync();
            }
        }

        public async Task IncrementDownloadCountAsync(Guid id)
        {
            var stats = await _db.TrackStats.FirstOrDefaultAsync(s => s.TrackId == id);
            if (stats != null)
            {
                stats.DownloadCount++;
                await _db.SaveChangesAsync();
            }
        }
    }