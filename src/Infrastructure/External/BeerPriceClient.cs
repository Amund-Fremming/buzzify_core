using Domain.Contracts;
using Domain.Entities.Hub;
using Domain.Shared.ResultPattern;

namespace Infrastructure.External;

public class BeerPriceClient(IHttpClientFactory factory) : IBeerPriceClient
{
    private readonly HttpClient _client = factory.CreateClient(nameof(BeerPrice));

    public async Task<Result<IEnumerable<BeerPrice>>> GetBeerPrices(string city)
    {
        try
        {
            var result = await _client.GetStringAsync("/oslo");
            return new List<BeerPrice>();
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetBeerPrices), ex);
        }
    }
}