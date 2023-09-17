using Autofac;
using Tempus.AppLayer.WorkTime.Queries;
using Module = Autofac.Module;

namespace Kafo.Server.API.AutofacModules;

/// <summary>
/// Модуль для регистрации компонентов из Application
/// </summary>
public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Application
        builder.RegisterAssemblyTypes(typeof(GetWorkTimesForDateQuery).Assembly)
            .Where(t => t.Name.EndsWith("Query"))
            .AsSelf();
        builder.RegisterAssemblyTypes(typeof(GetWorkTimesForDateQuery).Assembly)
            .Where(t => t.Name.EndsWith("Command"))
            .AsSelf();
        builder.RegisterAssemblyTypes(typeof(GetWorkTimesForDateQuery).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsSelf();
    }
}
