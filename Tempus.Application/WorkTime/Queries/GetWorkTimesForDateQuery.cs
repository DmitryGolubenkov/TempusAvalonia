using Tempus.Core.Entities.TimeManagement;
using Tempus.Core.Interfaces;

namespace Tempus.AppLayer.WorkTime.Queries;

/// <summary>
/// Возвращает все периоды работы за указанную дату
/// </summary>
public class GetWorkTimesForDateQuery
{
    private readonly IRepository<WorkPeriod> _workPeriods;

    public GetWorkTimesForDateQuery(IRepository<WorkPeriod> workPeriods)
    {
        _workPeriods = workPeriods;
    }

    public async Task<List<WorkPeriod>> Execute(DateTime date)
    {
        // Получаем данные, сортируем их и возвращаем. Отсортировать на уровне БД нельзя из-за использования SQLite
        return _workPeriods.Where(x=>x.Date.Date == date.Date).ToList().OrderBy(x=>x.StartTime).ToList();
    }
}
