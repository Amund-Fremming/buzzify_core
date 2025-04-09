using Domain.Abstractions;

namespace Domain.Entities.Shared;

public sealed class RegisteredUser : UserBase
{
    public string Auth0Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    private RegisteredUser()
    { }

    public static RegisteredUser Create(string authId, string name, string email)
        => new()
        {
            Guid = Guid.NewGuid(),
            LastActive = DateTime.Now,
            Auth0Id = authId,
            Name = name,
            Email = email
        };
}