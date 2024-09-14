using System.Text.Json.Serialization;

namespace AtScheduler.Shared;

public class Result<TValue, TError> where TError : Error
{
    public readonly TValue? Value;
    public readonly TError? Error;

    public bool IsSuccess;
    public bool IsFailure => !IsSuccess;

    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);
    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);

    public Result<TValue, TError> Match(Func<TValue, Result<TValue, TError>> success, Func<TError, Result<TValue, TError>> failure)
    {
        if (IsSuccess)
        {
            return success(Value!);
        }
        return failure(Error!);
    }
}

public record Error(
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("description")] string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "A null value was provided");
}