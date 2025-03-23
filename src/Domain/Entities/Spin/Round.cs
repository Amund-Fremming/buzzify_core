using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Round
{
    [Key]
    public int Id { get; set; }

    public int SpinGameId { get; set; }

    public bool Completed { get; set; }
}