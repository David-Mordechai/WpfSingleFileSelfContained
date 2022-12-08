using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WpfApp.Utils;
using WpfApp.ViewModels;

namespace WpfApp;

public partial class App
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                builder.Sources.Clear();
                builder.AddJsonFile("settings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddDebug();
            })
            .ConfigureServices((context, services) =>
            {
                var settings = context.Configuration.GetSection("appSettings").Get<AppSettings>();
                if (settings is null) throw new Exception("Fail to load appSettings section from settings.json file.");

                services.AddSingleton(settings);
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<MainWindow>();
            }).Build();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}