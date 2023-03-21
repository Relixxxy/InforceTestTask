namespace InforceTestTask.Infrastructure.Exceptions;

public class BusinessException : Exception
{
    public BusinessException()
    {
    }

    public BusinessException(string msg, int status)
        : base(msg)
    {
        Status = status;
    }

    public BusinessException(string message, Exception innerException)
    : base(message, innerException)
    {
    }

    public int Status { get; set; }
}
