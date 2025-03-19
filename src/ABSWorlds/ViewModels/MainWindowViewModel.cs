using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Dialogs;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ABSWorlds.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    //public string Greeting { get; } = "Welcome to Avalonia!";

    [ObservableProperty] private WindowState   _windowState;
    [ObservableProperty] private WindowState[] _windowStates = Array.Empty<WindowState>();

    [ObservableProperty]
    private ExtendClientAreaChromeHints _chromeHints = ExtendClientAreaChromeHints.PreferSystemChrome;

    [ObservableProperty] private bool      _extendClientAreaEnabled;
    [ObservableProperty] private bool      _systemTitleBarEnabled;
    [ObservableProperty] private bool      _preferSystemChromeEnabled;
    [ObservableProperty] private double    _titleBarHeight;
    [ObservableProperty] private bool      _isSystemBarVisible;
    [ObservableProperty] private bool      _displayEdgeToEdge;
    [ObservableProperty] private Thickness _safeAreaPadding;
    [ObservableProperty] private DateTime? _validatedDateExample;

    public MainWindowViewModel() {
        AboutCommand = new RelayCommand(async () => {
            var dialog = new AboutAvaloniaDialog();

            if ((Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?
                .MainWindow is { } mainWindow) {
                await dialog.ShowDialog(mainWindow);
            }
        });

        ExitCommand = new RelayCommand(() => {
            (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
        });

        WindowState = WindowState.Normal;

        WindowStates = [
                WindowState.Minimized,
                WindowState.Normal,
                WindowState.Maximized,
                WindowState.FullScreen];
        
        SystemTitleBarEnabled = true;
        TitleBarHeight        = -1;
    }

    public RelayCommand AboutCommand { get; }
    public RelayCommand ExitCommand  { get; }
}
