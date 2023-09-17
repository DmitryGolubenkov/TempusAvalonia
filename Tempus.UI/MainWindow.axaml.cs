using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using Tempus.AppLayer.Settings.Services;

namespace Tempus.UI
{
    public partial class MainWindow : Window
    {
        private bool isDarkMode = false;
        private readonly SettingsService _settingsService;

        public MainWindow(SettingsService settingsService)
        {
            _settingsService = settingsService;
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            isDarkMode = _settingsService.GetTheme().Result == "dark" ? true : false;
        }


        private void ChangeTheme(object? sender, RoutedEventArgs e)
        {
            isDarkMode = !isDarkMode;
            _settingsService.SetTheme(!isDarkMode ? "light" : "dark");

            if (!isDarkMode)
                SukiUI.ColorTheme.LoadLightTheme(Application.Current);
            else
                SukiUI.ColorTheme.LoadDarkTheme(Application.Current);

        }

    }
}