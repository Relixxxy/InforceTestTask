namespace InforceTestTask.Models;

public class ErrorVW
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}