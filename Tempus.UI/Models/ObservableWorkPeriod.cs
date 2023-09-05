using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Tempus.Core.Entities.TimeManagement;

namespace Tempus.UI.Models;

public partial class ObservableWorkPeriod : ObservableObject
{
    private readonly WorkPeriod _workPeriod;

    public WorkPeriod ToEntity() => _workPeriod;

    public ObservableWorkPeriod()
    {
        _workPeriod = new WorkPeriod() 
        { 
            Date = DateTime.Now.Date,
        };
    }

    public ObservableWorkPeriod(WorkPeriod workPeriod)
    {
        _workPeriod = workPeriod;
    }

    public TimeSpan? StartTime
    {
        get => _workPeriod.StartTime;
        set => SetProperty(_workPeriod.StartTime, value, _workPeriod, (u, va) => u.StartTime = (TimeSpan)va);
    }

    public TimeSpan? EndTime
    {
        get => _workPeriod.EndTime;
        set => SetProperty(_workPeriod.EndTime, value, _workPeriod, (u, va) => u.EndTime = (TimeSpan)va);
    }
    public DateTime Date
    {
        get => _workPeriod.Date;
        set => SetProperty(_workPeriod.Date, value, _workPeriod, (u, va) => u.Date = va);
    }
    public string? Note
    {
        get => _workPeriod.Note;
        set => SetProperty(_workPeriod.Note, value, _workPeriod, (u, va) => u.Note = va);
    }

    public TimeSpan? Total => _workPeriod.Total;

    public string? AsHours => _workPeriod.AsHours;

    public Guid Id { get => _workPeriod.Id;}
}