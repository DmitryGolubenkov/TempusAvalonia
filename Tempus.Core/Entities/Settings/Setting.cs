namespace Tempus.Core.Entities.Settings;

public class Setting : EntityBase
{
    /// <summary>
    /// Ключ, по которому можно найти настройку
    /// </summary>
    public required string Key { get; set; }

    /// <summary>
    /// Значение ключа
    /// </summary>
    public required string Value { get; set; }
}
