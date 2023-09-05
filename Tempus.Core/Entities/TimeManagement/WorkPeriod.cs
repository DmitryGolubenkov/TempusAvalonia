namespace Tempus.Core.Entities.TimeManagement;

/// <summary>
/// Период работы
/// </summary>
public class WorkPeriod : EntityBase
{
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Время начала периода
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Время окончания периода
    /// </summary>
    public TimeSpan? EndTime { get; set; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Время работы в периоде
    /// </summary>
    public TimeSpan? Total => StartTime < EndTime ? EndTime - StartTime : StartTime - EndTime;

    /// <summary>
    /// Время в часах, где 1 = 1 час, 0.5 = 30 минут
    /// </summary>
    public string? AsHours => Total?.TotalHours.ToString("0.##");
}
