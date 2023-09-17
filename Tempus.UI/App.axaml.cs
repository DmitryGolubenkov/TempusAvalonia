using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Tempus.Persistence;
using Microsoft.EntityFrameworkCore;
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

        Startup.ConfigureServices(builder);

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
}
