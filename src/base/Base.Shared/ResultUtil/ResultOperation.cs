namespace Base.Shared.ResultUtility;

/// <summary>
/// برگرداندن نتیجه عملیات انجام شده
/// </summary>
public class ResultOperation
{
    private ResultOperation()
    {
        
    }
    public bool isSuccess { get; private set; }

    public string message { get; private set; } = string.Empty;

    public static ResultOperation ToSuccessResult(string msg)
    {
        return new ResultOperation()
        {
            isSuccess = true,
            message =  msg
        };
    }
    public static ResultOperation ToSuccessResult()
    {
        return new ResultOperation()
        {
            isSuccess = true,
            
        };
    }
    public static ResultOperation ToFailedResult(string message)
    {
        return new ResultOperation()
        {
            isSuccess = false,
            message = message,
        };
    }
    public static ResultOperation ToFailedResult(List<string> message)
    {
        return new ResultOperation()
        {
            isSuccess = false,
            message = string.Join("\n", message)
        };
    }
    public static ResultOperation ToFailedResult(IEnumerable<string> message)
    {
        return new ResultOperation()
        {
            isSuccess = false,
            message = string.Join("\n", message)
        };
    }
}


/// <summary>
/// برگرداندن نتیجه عملیات به همراه دیتا خروجی از آن
/// </summary>
/// <typeparam name="T">نوع دیتایی که قصد برگرداندن آن را دارید</typeparam>
public class ResultOperation<T>
{
    public bool isSuccess { get; private set; }
    public string message { get; private set; }
    private ResultOperation()
    {
        message = string.Empty;
    }
    public T Data { get; private set; }
    public static ResultOperation<T> ToSuccessResult<T>(T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            isSuccess = true,
           
        };
    }
    public static ResultOperation<T> ToSuccessResult(string message, T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            isSuccess = true,
            message = message
        };
    }
    public static ResultOperation<T> ToSuccessResult(List<string> message, T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            isSuccess = true,
            message = string.Join("\n", message)
        };
    }
    public static ResultOperation<T> ToFailedResult()
    {
        return new ResultOperation<T>()
        {
           
            isSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(string message)
    {
        return new ResultOperation<T>()
        {
            message = message ,
            isSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(List<string> message)
    {
        return new ResultOperation<T>()
        {
            message = string.Join("\n", message),
            isSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(IEnumerable<string> message)
    {
        return new ResultOperation<T>()
        {
            message = string.Join("\n", message),
            isSuccess = false,
        };
    }
    public static ResultOperation<T> ToFailedResult(string message, T data)
    {
        return new ResultOperation<T>()
        {
            Data = data,
            isSuccess = false,
            message = message
        };
    }
    public static ResultOperation<T> ToFailedResult(List<string> message, T data)
    {
        return new ResultOperation<T>()
        {
            isSuccess = false,
            Data = data,
            message = string.Join("\n", message)
        };
    }
}
