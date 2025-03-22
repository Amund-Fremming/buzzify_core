namespace Domain.Exceptions;

public sealed class RepositoryNotFoundException(string name) : Exception($"Repository with name {name} was not found.");