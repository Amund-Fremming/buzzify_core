namespace Domain.Exceptions;

public sealed class EntityNotFoundException(int id) : Exception($"Entity with Id {id} was not found!");