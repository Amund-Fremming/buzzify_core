using Microsoft.AspNetCore.Mvc;

namespace Core.src.Shared.ResultPattern;

public static class ResultExtention
{
    /// <summary>
    /// Resolves the result of a given <see cref="Result"/> by executing the appropriate action based on its success or failure state.
    /// </summary>
    /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
    /// <param name="success">A function that takes the <see cref="Result"/> as input and returns a successful <see cref="Result"/> when the operation is successful.</param>
    /// <param name="error">A function that takes the <see cref="Result"/> as input and returns a failure <see cref="Result"/> when the operation fails.</param>
    /// <returns>An <see cref="Result"/> representing either a successful or failure response based on the outcome of the result.</returns>
    public static Result Resolve(this Result result, Func<Result, Result> success, Func<Result, Result> error)
        => result.IsError ? error(result) : success(result);

    /// <summary>
    /// Resolves the result of a given <see cref="Result{T}"/> by executing the appropriate action based on its success or failure state.
    /// </summary>
    /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
    /// <param name="success">A function that takes the <see cref="Result"/> as input and returns a successful <see cref="Result"/> when the operation is successful.</param>
    /// <param name="error">A function that takes the <see cref="Result"/> as input and returns a failure <see cref="Result"/> when the operation fails.</param>
    /// <returns>An <see cref="Result"/> representing either a successful or failure response based on the outcome of the result.</returns>
    public static Result<T> Resolve<T>(this Result<T> result, Func<Result<T>, Result<T>> success, Func<Result<T>, Result<T>> error)
        => result.IsError ? error(result) : success(result);

    /// <summary>
    /// Resolves the result of a given <see cref="Result{T}"/> by executing the appropriate action based on its success or failure state.
    /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
    /// </summary>
    /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
    /// <param name="success">A function that takes the <see cref="Result"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
    /// <param name="error">A function that takes the <see cref="Result"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
    /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
    public static ActionResult Resolve(this Result result, Func<Result, ActionResult> success, Func<Result, ActionResult> error)
        => result.IsError ? error(result) : success(result);

    /// <summary>
    /// Resolves the result of a given <see cref="Result{T}"/> by executing the appropriate action based on its success or failure state.
    /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
    /// </summary>
    /// <typeparam name="T">The type of data contained within the <see cref="Result{T}"/>.</typeparam>
    /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
    /// <param name="success">A function that takes the <see cref="Result{T}"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
    /// <param name="error">A function that takes the <see cref="Result{T}"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
    /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
    public static ActionResult Resolve<T>(this Result<T> result, Func<Result<T>, ActionResult> success, Func<Result<T>, ActionResult> error)
        => result.IsError ? error(result) : success(result);

    /// <summary>
    /// Changes type T to type U.
    /// </summary>
    public static Result<U> ToType<T, U>(this Result<T> result) => new(default!, result.Error!);

    /// <summary>
    /// Adds type T to the Result.
    /// </summary>
    public static Result<T> AddType<T>(this Result result) => new(default!, result.Error!);

    /// <summary>
    /// Removes type T from Result.
    /// </summary>
    public static Result RemoveType<T>(this Result<T> result) => new(result.Error);

    /// <summary>
    /// Unwraps the data inside the wrapped result.
    /// </summary>
    public static T UnWrap<T>(this Result<T> result) => result.Data;
}