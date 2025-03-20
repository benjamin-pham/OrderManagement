namespace OrderManagement.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public string Message { get; protected set; } = null!;
    public bool IsSuccess { get; protected set; }
    public static Result Success(string message = "Success")
    {
        return new Result(true, message);
    }

    public static Result Failure(string message = "Failure")
    {
        return new Result(false, message);
    }

    public static Result<T> Success<T>(T data, string message = "Success")
    {
        return new Result<T>(data, true, message);
    }
   

    public static Result<T?> Failure<T>(string message = "Failure")
    {
        return new Result<T?>(default, false, message);
    }
}
public class Result<TValue> : Result
{
    public Result(TValue value, bool isSuccess, string message)
        : base(isSuccess, message)
    {
        Value = value;
    }

    public TValue Value { get; }
}
