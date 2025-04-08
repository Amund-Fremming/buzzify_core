namespace Domain.DTOs;

public sealed record CreateNotificationRequest
{
    public int PassCode { get; set; }
    public string Heading { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}