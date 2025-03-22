namespace Domain.Shared.ResultPattern;

public sealed record Error(string Message, Exception? Exception = null);