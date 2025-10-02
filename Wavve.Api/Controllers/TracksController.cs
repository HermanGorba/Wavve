using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wavve.Core.Dtos.Track;
using Wavve.Core.Interfaces;

namespace Wavve.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TracksController : ControllerBase
{
    private readonly ITrackService _trackService;

    public TracksController(ITrackService trackService)
    {
        _trackService = trackService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tracks = await _trackService.GetAllAsync();
        return Ok(tracks);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var track = await _trackService.GetByIdAsync(id);
        if (track == null) return NotFound();
        return Ok(track);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Upload([FromBody] TrackUploadDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var track = await _trackService.UploadAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TrackUpdateDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var success = await _trackService.UpdateAsync(id, userId, dto);
        if (!success) return Forbid();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var success = await _trackService.DeleteAsync(id, userId);
        if (!success) return Forbid();
        return NoContent();
    }

    [HttpPost("{id:guid}/listen")]
    public async Task<IActionResult> IncrementListen(Guid id)
    {
        await _trackService.IncrementListenCountAsync(id);
        return NoContent();
    }

    [HttpPost("{id:guid}/download")]
    public async Task<IActionResult> IncrementDownload(Guid id)
    {
        await _trackService.IncrementDownloadCountAsync(id);
        return NoContent();
    }
}