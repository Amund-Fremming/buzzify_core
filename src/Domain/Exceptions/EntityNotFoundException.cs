namespace Domain.Exceptions;

public sealed class EntityNotFoundException(Guid id) : Exception($"Entity with Id {id} was not found!");