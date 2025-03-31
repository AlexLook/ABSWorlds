using System;
using System.Linq;
using ABSWorlds.Common.Models;
using ABSWorlds.ViewModels;
using ABSWorlds.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Avalonia.Themes.Simple;

namespace ABSWorlds;

public partial class App : Application {

    private readonly Styles       _themeStylesContainer = [];
    private          FluentTheme? _fluentTheme;
    private          SimpleTheme? _simpleTheme;
    private          IStyle?      _colorPickerFluent, _colorPickerSimple;
    private          IStyle?      _dataGridFluent,    _dataGridSimple;

    public App() {
        DataContext = new ApplicationViewModel();
    }

    public override void Initialize() {
        Styles.Add(_themeStylesContainer);
        
        AvaloniaXamlLoader.Load(this);
        
        _fluentTheme       = [];
        _simpleTheme       = [];
        _colorPickerFluent = (IStyle)Resources["ColorPickerFluent"]!;
        _colorPickerSimple = (IStyle)Resources["ColorPickerSimple"]!;
        _dataGridFluent    = (IStyle)Resources["DataGridFluent"]!;
        _dataGridSimple    = (IStyle)Resources["DataGridSimple"]!;

        SetCatalogThemes(VisualTheme.Fluent);
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(), };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation() {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove=
                BindingPlugins.DataValidators
                              .OfType<DataAnnotationsValidationPlugin>()
                              .ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove) {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
    
    private       VisualTheme _prevTheme;
    public static VisualTheme CurrentTheme => ((App)Current!)._prevTheme;

    public static void SetCatalogThemes(VisualTheme theme)
    {
        var app       = (App)Current!;
        var prevTheme = app._prevTheme;
        app._prevTheme = theme;
        var shouldReopenWindow = prevTheme != theme;

        if (app._themeStylesContainer.Count == 0)
        {
            app._themeStylesContainer.Add(new Style());
            app._themeStylesContainer.Add(new Style());
            app._themeStylesContainer.Add(new Style());
        }

        switch (theme) {
            case VisualTheme.Fluent:
                app._themeStylesContainer[0] = app._fluentTheme!;
                app._themeStylesContainer[1] = app._colorPickerFluent!;
                app._themeStylesContainer[2] = app._dataGridFluent!;
                break;
            case VisualTheme.Simple:
                app._themeStylesContainer[0] = app._simpleTheme!;
                app._themeStylesContainer[1] = app._colorPickerSimple!;
                app._themeStylesContainer[2] = app._dataGridSimple!;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
        }

        if (shouldReopenWindow) {
            switch (app.ApplicationLifetime) {
                case IClassicDesktopStyleApplicationLifetime desktopLifetime: {
                    var oldWindow = desktopLifetime.MainWindow;
                    var newWindow = new MainWindow();
                    desktopLifetime.MainWindow = newWindow;
                    newWindow.Show();
                    oldWindow?.Close();
                    break;
                }
                case ISingleViewApplicationLifetime singleViewLifetime:
                    singleViewLifetime.MainView = new MainView();
                    break;
            }
        }
    }
}
