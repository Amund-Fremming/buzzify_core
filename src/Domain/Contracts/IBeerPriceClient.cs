using Domain.Entities;
using Domain.Shared.ResultPattern;

namespace Domain.Contracts;

public interface IBeerPriceClient
{
    Task<Result<IEnumerable<BeerPrice>>> GetBeerPrices(string city);
}