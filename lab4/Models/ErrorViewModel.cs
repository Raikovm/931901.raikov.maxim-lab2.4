namespace lab4.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; } = string.Empty;
    public bool ShowRequestId 
        => !string.IsNullOrEmpty(RequestId);
}