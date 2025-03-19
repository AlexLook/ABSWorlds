using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;

namespace ABSWorlds.ViewModels;

public class ApplicationViewModel {
    public ApplicationViewModel()
    {
        ExitCommand = new RelayCommand(() =>
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime.Shutdown();
            }
        });
    }

    public RelayCommand ExitCommand { get; }
}
