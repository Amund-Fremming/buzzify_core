namespace Domain.Shared.ResultPattern;

public sealed class ResultException(string message) : Exception(message);