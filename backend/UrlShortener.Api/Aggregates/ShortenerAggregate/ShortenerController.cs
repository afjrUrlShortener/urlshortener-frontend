using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace UrlShortener.Api.Aggregates.ShortenerAggregate;

[ApiController]
[Route("api/v1/[controller]")]
public class ShortenerController : ControllerBase
{
    private readonly ShortenerOptions _options;
    private readonly IShortenerService _shortenerService;

    public ShortenerController(IOptions<ShortenerOptions> options, IShortenerService shortenerService)
    {
        _options = options.Value;
        _shortenerService = shortenerService;
    }

    [HttpPost]
    [ProducesResponseType<CreateShortenedUrlResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateShortenedUrl([FromBody] CreateShortenedUrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Url))
            return BadRequest($"{nameof(request.Url)} must not be null or empty");

        if (!request.Url.StartsWith("https://") && !request.Url.StartsWith("http://"))
            return BadRequest($"{nameof(request.Url)} must start with https:// or http://");

        var shortUrl = await _shortenerService.CreateShortUrl(request.Url, request.ExpiresAt?.UtcDateTime);
        if (string.IsNullOrWhiteSpace(shortUrl))
            return Problem("Failed to create short url");

        return Created(
            $"{_options.ShortenerDomain}/{shortUrl}",
            new CreateShortenedUrlResponse(shortUrl)
        );
    }

    [HttpGet("{shortUrl}")]
    [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RedirectToLongUrl([FromRoute] string shortUrl)
    {
        if (string.IsNullOrWhiteSpace(shortUrl))
            return BadRequest();

        var longUrl = await _shortenerService.GetLongUrl(shortUrl);
        if (string.IsNullOrWhiteSpace(longUrl))
            return NotFound();

        return RedirectPermanentPreserveMethod(longUrl);
    }
}