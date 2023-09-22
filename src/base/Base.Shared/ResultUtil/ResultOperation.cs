namespace Base.Shared.ResultUtility;

/// <summary>
/// برگرداندن نتیجه عملیات انجام شده
/// </summary>
public class ResultOperation
{
    private ResultOperation()
    {
        Message = new List<string>();
    }
    public bool IsSuccess { get; private set; }
    public IEnumerable<string>? Message { get; private set; }

    public static ResultOperation ToSuccessResult(string message)
    {
        return new ResultOperation()
        {
            IsSuccess = true,
            Message = new List<string>() { message },
        };
    }
    public static ResultOperation ToSuccessResult()
    {
        return new ResultOperation()
        {
            IsSuccess = true,
            Message = new List<string>()
        };
    }
    public static ResultOperation ToFailedResult(string message)
    {
        return new ResultOperation()
        {
            IsSuccess = false,
            Message = new List<string>() { message },
        };
    }
    public static ResultOperation ToFailedResult(List<string> message)
    {
        return new ResultOperation()
        {
            IsSuccess = false,
            Message = message,
        };
    }
    public static ResultOperation ToFailedResult(IEnumerable<string> message)
    {
        return new ResultOperation()
        {
            IsSuccess = false,
            Message = message,
        };
    }
}


/// <summary>
/// برگرداندن نتیجه عملیات به همراه دیتا خروجی از آن
/// </summary>
/// <typeparam name="T">نوع دیتایی که قصد برگرداندن آن را دارید</typeparam>
public class ResultOperation<T>
{
    public bool IsSuccess { get; private set; }
    public IEnumerable<string>? Message { get; private set; }
    private ResultOperation()
    {
        Message = new List<string>();
    }
    public T Data { get; private set; }
    public static ResultOperation<T> ToSuccessResult<T>(T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = new List<string>()
        };
    }
    public static ResultOperation<T> ToSuccessResult(string message, T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = new List<string>() { message },
        };
    }
    public static ResultOperation<T> ToSuccessResult(List<string> message, T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = message,
        };
    }
    public static ResultOperation<T> ToFailedResult()
    {
        return new ResultOperation<T>()
        {
            Message = new List<string>() { },
            IsSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(string message)
    {
        return new ResultOperation<T>()
        {
            Message = new List<string>() { message },
            IsSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(List<string> message)
    {
        return new ResultOperation<T>()
        {
            Message = message,
            IsSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(IEnumerable<string> message)
    {
        return new ResultOperation<T>()
        {
            Message = message,
            IsSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(string message, T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            IsSuccess = false,
            Message = new List<string>() { message }
        };
    }
    public static ResultOperation<T> ToFailedResult(List<string> message, T data)
    {
        return new ResultOperation<T>()
        {
            IsSuccess = false,
            Data = data,
            Message = message
        };
    }
}
