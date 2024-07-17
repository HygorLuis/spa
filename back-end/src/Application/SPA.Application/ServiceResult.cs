namespace SPA.Application;

public class ServiceResult<T>
{
    public bool Succeeded { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }

    private ServiceResult(bool succeeded, T? data, string? errorMessage)
    {
        Succeeded = succeeded;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static ServiceResult<T> Success(T? data = default) => new(true, data, null);

    public static ServiceResult<T> Failure(string errorMessage, T? data = default) => new(false, data, errorMessage);
}