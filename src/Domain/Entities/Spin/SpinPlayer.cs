using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinPlayer
{
    [Key]
    public int Id { get; set; }

    public int SpinGameId { get; set; }
    public int PlayerId { get; set; }
}