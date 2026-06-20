namespace BookingsApi.Models;

public enum ResultType
{
    Success,
    NotFound,
    ValidationError,
    Unauthorized,
    Conflict
}

public class Result
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public ResultType Type { get; }

    protected Result(bool isSuccess, ResultType type, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        Type = type;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new(true, ResultType.Success);
    public static Result NotFound(string message) => new(false, ResultType.NotFound, message);
    public static Result ValidationError(string message) => new(false, ResultType.ValidationError, message);
    public static Result Unauthorized(string message) => new(false, ResultType.Unauthorized, message);
    public static Result Conflict(string message) => new(false, ResultType.Conflict, message);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, ResultType type, T? value = default, string? errorMessage = null)
        : base(isSuccess, type, errorMessage)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, ResultType.Success, value);
    public new static Result<T> NotFound(string message) => new(false, ResultType.NotFound, errorMessage: message);
    public new static Result<T> ValidationError(string message) => new(false, ResultType.ValidationError, errorMessage: message);
    public new static Result<T> Unauthorized(string message) => new(false, ResultType.Unauthorized, errorMessage: message);
    public new static Result<T> Conflict(string message) => new(false, ResultType.Conflict, errorMessage: message);
}
