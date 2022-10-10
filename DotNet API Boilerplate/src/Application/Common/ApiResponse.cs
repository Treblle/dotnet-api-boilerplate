namespace DotNet_API_Boilerplate.Core.Common;

public class ApiResponse<T> where T : class
{
    public ApiResponse()
    {
    }
    public ApiResponse(bool status, string message, T @object)
    {
        Status = status;
        Message = message;
        Object = @object;
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public T Object { get; set; }
}
