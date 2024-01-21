using Domain.Common.Interfaces;


namespace Domain.Exceptions
{
public class HttpException : Exception, IHttpException
{
    private readonly int _statusCode;
    public HttpException() : base("unknown error occurred!")
    {
        _statusCode = 500;
    }
    public HttpException(int statusCode, string message) : base(message)
    {
        _statusCode = statusCode;
    }

    public HttpException(int statusCode, string message, Exception innerException) : base(message, innerException)
    {
        _statusCode = statusCode;
    }

    public HttpException(string message) : base(message)
    {
        _statusCode = 500;
    }

    public HttpException(string message, Exception innerException) : base(message, innerException)
    {
        _statusCode = 500;
    }
    public int StatusCode => _statusCode;
}
}

