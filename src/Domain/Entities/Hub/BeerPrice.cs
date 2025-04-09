using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Hub;

public class BeerPrice
{
    [Key]
    public int Id { get; set; }

    public string? City { get; set; }
    public string? Region { get; set; }
    public string? Price { get; set; }

    private BeerPrice()
    { }

    public static BeerPrice Create(string city, string region, string price)
        => new()
        {
            City = city,
            Region = region,
            Price = price
        };
}