using System.Text.RegularExpressions;

namespace API_Host.Tools;

/// <summary>
/// Класс, определяющий ограничение для ввода данных в URL.
/// Чтобы фигню там не писали и чтобы лишний раз не проверять на правильность ввода
/// </summary>
public sealed class HashIDConstraint : IRouteConstraint
{
    private readonly IConfiguration _config;

    public HashIDConstraint (IConfiguration config)
    {
        _config = config;
    }

    public bool Match (HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        Regex regex = new(_config["Regex:HashID"]!);
        
        return regex.IsMatch(values[routeKey]?.ToString()!);
    }
}