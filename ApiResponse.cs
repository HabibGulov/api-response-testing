
public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public List<string>? Messages { get; set; } = [];
    public T? Data { get; set; }

    private ApiResponse(bool isSuccess, string? message, List<string>? messages, T? data)
    {
        IsSuccess = isSuccess;
        if (message != null) Messages?.Add(message);
        Messages = messages;
        Data = data;
    }

    public static ApiResponse<T> Success(List<string>? messages, T data, string message = "Success")
    {
        return new ApiResponse<T>(true, message, messages, data);
    }

    public static ApiResponse<T> Fail(List<string>? messages, T? data, string message = "Fail")
    {
        return new ApiResponse<T>(false, message, messages, data);
    }
}