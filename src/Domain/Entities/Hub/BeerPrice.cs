using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Hub;

public class BeerPrice
{
    [Key]
    public int Id { get; set; }

    public string? City { get; set; }
    public string? Region { get; set; }
    public string? Price { get; set; }
}