﻿namespace Domain.Shared.ResultPattern;

public sealed record Result<T>(T Data, Error Error, EmptyResult? EmptyResult = null) : IResult, IResult<T>
{
    public bool IsEmpty => EmptyResult != null;
    public bool IsError => Error is not null && Error.Message is not null;

    public string Message => Error!.Message;

    public static Result<T> Ok(T data) => new(data, null!);

    public static implicit operator Result<T>(T data) => new(data, null!);

    public static implicit operator Result<T>(Error error) => new(default!, error);

    public static implicit operator Result<T>(EmptyResult emptyResult) => new(default!, new Error("Entity does not exist"), emptyResult);
}

public sealed record Result(Error Error) : IResult
{
    public bool IsError => Error is not null && Error.Exception is not null;

    public string Message => Error == null ? "No error message present." : Error.Message;

    public static Result Ok => new(Error: null!);

    public static implicit operator Result(Error error) => new(error);

    public static Result operator &(Result left, Result right)
    {
        if (left.IsError)
        {
            return left;
        }

        return right;
    }
}