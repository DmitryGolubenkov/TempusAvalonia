using Tempus.Persistence;
using Microsoft.EntityFrameworkCore;
using Tempus.Core.Interfaces;
using Autofac;
using System;

namespace Tempus.UI;

/// <summary>
/// Класс, в котором содержатся все зависимости приложения
/// </summary>
public static class Startup
{
    public static ContainerBuilder ConfigureServices(ContainerBuilder builder)
    {
        builder.RegisterAssemblyModules(typeof(App).Assembly);

        //Register all view models
        builder.RegisterAssemblyTypes(typeof(App).Assembly)
            .Where(t => t.Name.EndsWith("ViewModel"))
            .AsSelf()
            .AsImplementedInterfaces();

        //Register db context and repositories
        builder
            .RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();


        // Path to db should be in app folder
        var path = $"{AppDomain.CurrentDomain.BaseDirectory}/app.db";
        var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={path};")
                .Options;

        builder
            .RegisterType<AppDbContext>()
            .WithParameter("options", options)
            .InstancePerLifetimeScope();

        builder.RegisterType<MainWindow>()
            .SingleInstance();

        return builder;
    }

}