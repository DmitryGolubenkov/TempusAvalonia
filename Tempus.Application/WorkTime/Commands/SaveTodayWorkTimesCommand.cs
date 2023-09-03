using Tempus.Core.Entities.TimeManagement;
using Tempus.Core.Interfaces;

namespace Tempus.AppLayer.WorkTime.Commands;
public class SaveTodayWorkTimesCommand
{
    private readonly IRepository<WorkPeriod> _workPeriodRepository;

    public SaveTodayWorkTimesCommand(IRepository<WorkPeriod> workPeriodRepository)
    {
        _workPeriodRepository = workPeriodRepository;
    }

    public async Task Execute(List<WorkPeriod> workPeriods)
    {
        // Удаляем всё за сегодня
        var idsToRemove = _workPeriodRepository
            .Where(x => x.Date == DateTime.Today)
            .Select(x => x.Id)
            .ToList();

        foreach (var id in idsToRemove)
            _workPeriodRepository.RemoveById(id);



        // Добавляем новые данные
        foreach (var workPeriod in workPeriods)
        {
            workPeriod.Date = DateTime.Today;
            _workPeriodRepository.Add(workPeriod);
        }

        await _workPeriodRepository.SaveChangesAsync();
    }
}
