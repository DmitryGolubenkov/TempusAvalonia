using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Tempus.Persistence;
using Microsoft.EntityFrameworkCore;
using Tempus.Core.Interfaces;
using Autofac;

namespace Tempus.UI;

public class App : Application
{
    private IContainer _services;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {


        var builder = new ContainerBuilder();

        ConfigureServices(builder);

        var services = builder.Build();
        _services = services;
        // Resolver is used in DI and MVVM to resolve viewmodels for views.
        DISource.Resolver = services.Resolve;

        // Resolve and show main window

        // Persistence - Migrate database
        using (var scope = _services.BeginLifetimeScope())
        {
            var dataContext = scope.Resolve<AppDbContext>();
            dataContext.Database.Migrate();
        }

        // Настраиваем главное окно
        var mainWindow = services.Resolve<MainWindow>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = mainWindow;
        }
        mainWindow.Show();

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(ContainerBuilder builder)
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


        var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=c:\\app.db;")
                .Options;

        builder
            .RegisterType<AppDbContext>()
            .WithParameter("options", options)
            .InstancePerLifetimeScope();

        builder.RegisterType<MainWindow>()
            .SingleInstance();

    }
}