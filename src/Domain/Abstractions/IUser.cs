namespace Domain.Abstractions;

public interface IUser
{
    public int Id { get; }
    public Guid Guid { get; }
    public DateTime LastActive { get; }
}