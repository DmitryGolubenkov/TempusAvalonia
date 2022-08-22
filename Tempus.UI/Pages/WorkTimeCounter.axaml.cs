using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Tempus.UI.Pages;

public partial class WorkTimeCounter : UserControl
{
    public WorkTimeCounter()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
