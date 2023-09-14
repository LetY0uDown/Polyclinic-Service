namespace API_Host.Models;

public record class LoginData
{
    public LoginData (string eMail, string password)
    {
        EMail = eMail;
        Password = password;
    }

    public string EMail { get; private init; } = null!;

    public string Password { get; private init; } = null!;

    public bool IsEmpty => string.IsNullOrWhiteSpace(EMail) || string.IsNullOrWhiteSpace(Password);
}