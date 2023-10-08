using System.ComponentModel.DataAnnotations;

namespace DTO;

public class ClientDTO : Tools.Flags.DTO
{
    public Guid ID { get; set; }

    [RegularExpression("^[A-Za-z0-9_-]{5,25}$", ErrorMessage = "Логин задан неправильно")]
    public string Login { get; set; } = string.Empty;

    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,10}$", ErrorMessage = "Неверный E-Mail")]
    public string Email { get; set; } = string.Empty;

    [RegularExpression("^[А-Я]{1}[а-я\\s\\-]{0,50}$", ErrorMessage = "У вас имя неправильное")]
    public string? Name { get; set; } = string.Empty;

    [RegularExpression("^[А-Я]{1}[а-я\\s\\-]{0,50}$", ErrorMessage = "У вас фамилия неправильная")]
    public string? LastName { get; set; } = string.Empty;

    [RegularExpression("^[А-Я]{1}[а-я\\s\\-]{0,50}$", ErrorMessage = "У вас отчество неправильное")]
    public string? Patronymic { get; set; } = string.Empty;
}