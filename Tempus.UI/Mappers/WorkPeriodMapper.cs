using Riok.Mapperly.Abstractions;
using System.Linq;
using Tempus.Core.Entities.TimeManagement;
using Tempus.UI.Models;

namespace Tempus.UI.Mappers;

[Mapper]
internal static partial class WorkPeriodMapper
{
    public static partial ObservableWorkPeriod MapToObservable(WorkPeriod workPeriod);
    public static partial WorkPeriod MapToEntity(ObservableWorkPeriod observableWorkPeriod);
    public static partial IQueryable<ObservableWorkPeriod> Project(this IQueryable<WorkPeriod> q);
    public static partial IQueryable<WorkPeriod> Project(this IQueryable<ObservableWorkPeriod> q);
}

