using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Tempus.UI.Pages;
public partial class WorktimeHistoryPage : UserControl
{
    public WorktimeHistoryPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
