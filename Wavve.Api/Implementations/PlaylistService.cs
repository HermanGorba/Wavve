using Microsoft.EntityFrameworkCore;
using Wavve.Api.Data;
using Wavve.Core.Dtos.Playlist;
using Wavve.Core.Dtos.Track;
using Wavve.Core.Interfaces;
using Wavve.Core.Models;

namespace Wavve.Api.Implementations;

public class PlaylistService : IPlaylistService
    {
        private readonly ApplicationDbContext _db;

        public PlaylistService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<PlaylistDto> CreateAsync(Guid userId, PlaylistCreateDto dto)
        {
            var playlist = new Playlist
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Playlists.Add(playlist);
            await _db.SaveChangesAsync();

            return new PlaylistDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                UserId = playlist.UserId,
                TrackCount = 0
            };
        }

        public async Task<PlaylistDetailsDto?> GetByIdAsync(Guid id)
        {
            var playlist = await _db.Playlists
                .Include(p => p.User)
                .Include(p => p.PlaylistTracks)
                    .ThenInclude(pt => pt.Track)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null) return null;

            return new PlaylistDetailsDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                UserId = playlist.UserId,
                CreatedAt = playlist.CreatedAt,
                UserName = playlist.User?.UserName,
                TrackCount = playlist.PlaylistTracks.Count,
                Tracks = playlist.PlaylistTracks
                    .OrderBy(pt => pt.Order)
                    .Select(pt => new TrackDto
                    {
                        Id = pt.Track.Id,
                        Title = pt.Track.Title,
                        Description = pt.Track.Description,
                        FileUrl = pt.Track.FileUrl,
                        PreviewUrl = pt.Track.PreviewUrl,
                        Duration = pt.Track.Duration,
                        UploadedAt = pt.Track.UploadedAt,
                        UserId = pt.Track.UserId,
                        GenreId = pt.Track.GenreId
                    })
                    .ToList()
            };
        }

        public async Task<IEnumerable<PlaylistDto>> GetAllAsync()
        {
            return await _db.Playlists
                .Select(p => new PlaylistDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    UserId = p.UserId,
                    TrackCount = p.PlaylistTracks.Count
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, Guid userId, PlaylistUpdateDto dto)
        {
            var playlist = await _db.Playlists.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (playlist == null) return false;

            playlist.Name = dto.Name;
            playlist.Description = dto.Description;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var playlist = await _db.Playlists.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (playlist == null) return false;

            _db.Playlists.Remove(playlist);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTrackAsync(Guid playlistId, Guid userId, PlaylistAddTrackDto dto)
        {
            var playlist = await _db.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId && p.UserId == userId);
            if (playlist == null) return false;

            var exists = await _db.PlaylistTracks.AnyAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == dto.TrackId);
            if (exists) return false;

            var playlistTrack = new PlaylistTrack
            {
                Id = Guid.NewGuid(),
                PlaylistId = playlistId,
                TrackId = dto.TrackId,
                Order = dto.Order
            };

            _db.PlaylistTracks.Add(playlistTrack);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTrackAsync(Guid playlistId, Guid userId, Guid trackId)
        {
            var playlist = await _db.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId && p.UserId == userId);
            if (playlist == null) return false;

            var playlistTrack = await _db.PlaylistTracks.FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
            if (playlistTrack == null) return false;

            _db.PlaylistTracks.Remove(playlistTrack);
            await _db.SaveChangesAsync();
            return true;
        }
    }