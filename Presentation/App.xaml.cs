using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Presentation;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services.AddSingleton(this);
        Services.AddTransient<MainWindow>();

        Provider = Services.BuildServiceProvider();
    }

    private ServiceCollection Services { get; } = new();
    private ServiceProvider Provider { get; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var window = Provider.GetRequiredService<MainWindow>();
        window.Show();
    }
}
