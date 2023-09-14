namespace API_Host.Services.Interfaces;

public interface IValidator<T> where T : class
{
    bool IsValid (T obj, out string error);
}