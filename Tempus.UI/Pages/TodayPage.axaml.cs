using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Tempus.UI.Pages;
public partial class TodayPage : UserControl
{
    public TodayPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
