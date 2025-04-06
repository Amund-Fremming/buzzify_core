namespace Domain.Entities.Admin;

public sealed record UserActivityData
{
    public int Today { get; set; }
    public int Weekly { get; set; }
    public int Monthly { get; set; }
}