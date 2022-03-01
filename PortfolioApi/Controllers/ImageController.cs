using Microsoft.AspNetCore.Mvc;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private static readonly string[] Titles = new[]
    {
        "Seeds For Tomorrow", "finally designed a logo for myself", "The devil is in the details", "the joy of ducklings"
    };

    private readonly ILogger<ImageController> _logger;

    public ImageController(ILogger<ImageController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetImages")]
    public IEnumerable<Image> Get()
    {
        return Enumerable.Range(1, 3).Select(index => new Image
        {
            Title = Titles[Random.Shared.Next(Titles.Length)],
            Tags = new[]{"Volvo", "BMW", "Ford", "Mazda"},
            LinkURL = "https://www.instagram.com/viktorvx9/"
        })
        .ToArray();
    }
}
