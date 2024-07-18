using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Aggregates.ShortenerAggregate;

[ApiController]
[Route("api/v1/[controller]")]
public class ShortenerController : ControllerBase
{
    private readonly ILogger<ShortenerController> _logger;

    public ShortenerController(ILogger<ShortenerController> logger)
    {
        _logger = logger;
    }

    public record CreateShortenedUrlRequest(string Url);

    public record CreateShortenedUrlResponse(string Url);

    [HttpPost]
    [ProducesResponseType<CreateShortenedUrlResponse>(StatusCodes.Status201Created)]
    public Task<IActionResult> CreateShortenedUrl([FromBody] CreateShortenedUrlRequest request)
    {
        throw new NotImplementedException();
    }

    public record RedirectToShortenedUrlRequest(string Url);

    [HttpGet]
    public Task<IActionResult> RedirectToShortenedUrl([FromBody] RedirectToShortenedUrlRequest request)
    {
        throw new NotImplementedException();
    }
}