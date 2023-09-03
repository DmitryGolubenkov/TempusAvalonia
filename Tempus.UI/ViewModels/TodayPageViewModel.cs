using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tempus.AppLayer.WorkTime.Commands;
using Tempus.AppLayer.WorkTime.Queries;
using Tempus.UI.Models;
using Tempus.UI.Utilities;

namespace Tempus.UI.ViewModels;


public partial class TodayPageViewModel : ObservableObject
{
    #region Constructor

    public TodayPageViewModel(GetWorkTimesForDateQuery getWorkTimesForDateQuery, SaveTodayWorkTimesCommand saveTodayWorkTimesCommand)
    {
        _getWorkTimesForDateQuery = getWorkTimesForDateQuery;
        _saveTodayWorkTimesCommand = saveTodayWorkTimesCommand;

        var data = _getWorkTimesForDateQuery.Execute(DateTime.Now).Result.Select(x=> new ObservableWorkPeriod(x));
        workPeriods = new ObservableCollection<ObservableWorkPeriod>(data.ToList());

        workPeriods.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalWorkTimeTimeSpan));
        workPeriods.ToList().ForEach(x => x.PropertyChanged += async (s, e) => await DataChanged());
    }

    #endregion

    [ObservableProperty]
    private ObservableCollection<ObservableWorkPeriod> workPeriods;
    private readonly GetWorkTimesForDateQuery _getWorkTimesForDateQuery;
    private readonly SaveTodayWorkTimesCommand _saveTodayWorkTimesCommand;


    [ObservableProperty]
    private TimeSpan targetTime = new TimeSpan(8, 0, 0);

    #region Computed

    /// <summary>
    /// Всего отработано за день
    /// </summary>
    private string TotalWorkTimeTimeSpan => new TimeSpan(
        WorkPeriods.Where(x=>x.Total is not null).
        Sum(x => ((TimeSpan)x.Total!).Ticks))
        .ToString();

    private string WorkhoursLeft => (targetTime - new TimeSpan(WorkPeriods.Where(x => x.Total is not null)
        .Sum(x => ((TimeSpan)x.Total!).Ticks))).ToString();



    #endregion

    /// <summary>
    /// Добавляет новый период работы, устанавливая текущее время в качестве начала
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task AddWorkPeriod()
    {
        // Создаем новый элемент
        var newItem = new ObservableWorkPeriod()
        {
            Date = DateTime.Now.Date,
            StartTime = DateTime.Now.TimeOfDay,
        };

        // Добавляем в UI
        WorkPeriods.InsertInPlace<ObservableWorkPeriod, TimeSpan>(newItem, x=> (TimeSpan)x.StartTime);

        // Подписываемся на изменения
        newItem.PropertyChanged += async (s, e) => await DataChanged();
        await DataChanged();
    }

    /// <summary>
    /// Устанавливает переданному времени начало работы в текущее время
    /// </summary>
    [RelayCommand]
    private async Task SetStartForWorkPeriod(Guid id)
    {
        WorkPeriods.Where(x => x.Id == id).Single().StartTime = DateTime.Now.TimeOfDay;
        await DataChanged();
    }

    /// <summary>
    /// Устанавливает переданному времени конец работы в текущее время
    /// </summary>
    [RelayCommand]
    private async Task SetEndForWorkPeriod(Guid id)
    {
        WorkPeriods.Where(x => x.Id == id).Single().EndTime = DateTime.Now.TimeOfDay;
        await DataChanged();
    }

    /// <summary>
    /// Удаляет период работы из списка
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [RelayCommand]
    private async Task DeleteWorkPeriod(Guid id)
    {
        // Находим
        var workPeriod = WorkPeriods.Where(x => x.Id == id).Single();

        // Отписываемся 
        workPeriod.PropertyChanged -= async (s, e) => await DataChanged();

        // Удаляем
        WorkPeriods.Remove(workPeriod);

        // Сохраняем
        await DataChanged();
    }

    /// <summary>
    /// Вызывается при любом изменении данных и выполняет такие действия, как обновление UI, сохранение данных в БД и т.д.
    /// </summary>
    private async Task DataChanged()
    {
        //WorkPeriods.Sort<ObservableWorkPeriod>((a,b) => { return }));
        OnPropertyChanged(nameof(TotalWorkTimeTimeSpan));
        OnPropertyChanged(nameof(WorkhoursLeft));
        await _saveTodayWorkTimesCommand.Execute(WorkPeriods.Select(x=>x.ToEntity()).ToList());
    }



}


