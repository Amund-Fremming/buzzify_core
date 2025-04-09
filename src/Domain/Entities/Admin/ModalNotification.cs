namespace Domain.Entities.Admin;

public class ModalNotification
{
    public int Id { get; set; }
    public string Heading { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public string OutlineHexColor { get; private set; } = string.Empty;
    public bool DisplayNotification { get; private set; }

    public void SetDisplayNotification(bool displayNotification) => DisplayNotification = displayNotification;

    private ModalNotification()
    { }

    public static ModalNotification Create(string heading, string message, string outlineHexColor)
        => new()
        {
            Heading = heading,
            Message = message,
            OutlineHexColor = outlineHexColor,
            DisplayNotification = true
        };
}