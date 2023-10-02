using System.Text.Json;

namespace API_Host.Tools;

public record class ErrorDetails(int StatusCode, string Message)
{
    public static implicit operator string (ErrorDetails errorDetails)
    {
        return errorDetails.ToString();
    }

    public override string ToString ()
    {
        return JsonSerializer.Serialize(this);
    }
}