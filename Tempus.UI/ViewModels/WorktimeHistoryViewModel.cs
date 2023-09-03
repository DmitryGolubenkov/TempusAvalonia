using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tempus.AppLayer.WorkTime.Queries;
using Tempus.Core.Entities.TimeManagement;

namespace Tempus.UI.ViewModels;
public partial class WorktimeHistoryViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<WorkPeriod> worktimeData = new ObservableCollection<WorkPeriod>();


    [ObservableProperty]   
    private DateTime selectedDate = DateTime.Today;
    
    partial void OnSelectedDateChanged(DateTime value)
    {
        Refresh();
    }

    private readonly GetWorkTimesForDateQuery _getWorkTimesForDateQuery;

    public WorktimeHistoryViewModel(GetWorkTimesForDateQuery getWorkTimesForDateQuery)
    {
        _getWorkTimesForDateQuery = getWorkTimesForDateQuery;
    }

    [RelayCommand]
    public async Task Refresh()
    {
        WorktimeData = new ObservableCollection<WorkPeriod>(await _getWorkTimesForDateQuery.Execute(SelectedDate));
        OnPropertyChanged(nameof(WorktimeData));
    }

}
