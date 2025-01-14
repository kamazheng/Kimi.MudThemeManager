// ***********************************************************************
// <copyright file="MudThemeManager.razor.cs" company="Molex(Chengdu)">
//     Copyright © Molex(Chengdu) 2025
// </copyright>
// ***********************************************************************
// Author           : MOLEX\kzheng
// Created          : 01/13/2025
// ***********************************************************************

using Microsoft.AspNetCore.Components;
using MudBlazor.State;
using MudBlazor.ThemeManager.Extensions;
using System.Collections.ObjectModel;

namespace MudBlazor.ThemeManager;

/// <summary>
/// Represents the MudThemeManager component.
/// </summary>
public partial class MudThemeManager : ComponentBaseWithState
{
    private static readonly PaletteLight DefaultPaletteLight = new();
    private static readonly PaletteDark DefaultPaletteDark = new();
    private readonly ParameterState<bool> _openState;
    private readonly ParameterState<bool> _isDarkModeState;

    private PaletteLight? _currentPaletteLight;
    private PaletteDark? _currentPaletteDark;
    private Palette _currentPalette;
    private MudTheme? _customTheme;

    /// <summary>
    /// Initializes a new instance of the <see cref="MudThemeManager"/> class.
    /// </summary>
    public MudThemeManager()
    {
        using var registerScope = CreateRegisterScope();
        _openState = registerScope.RegisterParameter<bool>(nameof(Open))
            .WithParameter(() => Open)
            .WithEventCallback(() => OpenChanged);
        _isDarkModeState = registerScope.RegisterParameter<bool>(nameof(IsDarkMode))
            .WithParameter(() => IsDarkMode)
            .WithChangeHandler(OnIsDarkModeChanged);
        _currentPalette = GetPalette();
    }

    /// <summary>
    /// Gets or sets the theme presets.
    /// </summary>
    public virtual string ThemePresets { get; set; } = "Not Implemented";

    /// <summary>
    /// Gets or sets a value indicating whether the theme manager is open.
    /// </summary>
    [Parameter]
    public virtual bool Open { get; set; }

    /// <summary>
    /// Gets or sets the event callback for the Open property.
    /// </summary>
    [Parameter]
    public virtual EventCallback<bool> OpenChanged { get; set; }

    /// <summary>
    /// Gets or sets the theme.
    /// </summary>
    [Parameter]
    public virtual ThemeManagerTheme? Theme { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dark mode is enabled.
    /// </summary>
    [Parameter]
    public virtual bool IsDarkMode { get; set; }

    /// <summary>
    /// Gets or sets the color picker view.
    /// </summary>
    [Parameter]
    public virtual ColorPickerView ColorPickerView { get; set; } = ColorPickerView.Spectrum;

    /// <summary>
    /// Gets or sets the event callback for the Theme property.
    /// </summary>
    [Parameter]
    public virtual EventCallback<ThemeManagerTheme> ThemeChanged { get; set; }

    /// <summary>
    /// Gets or sets the font family list.
    /// </summary>
    [Parameter]
    public virtual ReadOnlyCollection<string> FontFamailyList { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _currentPalette = GetPalette();

        if (Theme is null)
        {
            return;
        }

        _customTheme = Theme.Theme.DeepClone();
        _currentPaletteLight = Theme.Theme.PaletteLight.DeepClone();
        _currentPaletteDark = Theme.Theme.PaletteDark.DeepClone();
    }

    /// <summary>
    /// Updates the palette based on the provided value.
    /// </summary>
    /// <param name="value">The theme updated value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public virtual Task UpdatePalette(ThemeUpdatedValue value)
    {
        UpdateCustomTheme();

        if (Theme is null || _customTheme is null)
        {
            return Task.CompletedTask;
        }

        Palette newPalette = _isDarkModeState.Value ? _customTheme.PaletteDark : _customTheme.PaletteLight;

        switch (value.ThemePaletteColor)
        {
            case ThemePaletteColor.Primary:
                newPalette.Primary = value.ColorStringValue;
                break;
            case ThemePaletteColor.Secondary:
                newPalette.Secondary = value.ColorStringValue;
                break;
            case ThemePaletteColor.Tertiary:
                newPalette.Tertiary = value.ColorStringValue;
                break;
            case ThemePaletteColor.Info:
                newPalette.Info = value.ColorStringValue;
                break;
            case ThemePaletteColor.Success:
                newPalette.Success = value.ColorStringValue;
                break;
            case ThemePaletteColor.Warning:
                newPalette.Warning = value.ColorStringValue;
                break;
            case ThemePaletteColor.Error:
                newPalette.Error = value.ColorStringValue;
                break;
            case ThemePaletteColor.Dark:
                newPalette.Dark = value.ColorStringValue;
                break;
            case ThemePaletteColor.Surface:
                newPalette.Surface = value.ColorStringValue;
                break;
            case ThemePaletteColor.Background:
                newPalette.Background = value.ColorStringValue;
                break;
            case ThemePaletteColor.BackgroundGray:
                newPalette.BackgroundGray = value.ColorStringValue;
                break;
            case ThemePaletteColor.DrawerText:
                newPalette.DrawerText = value.ColorStringValue;
                break;
            case ThemePaletteColor.DrawerIcon:
                newPalette.DrawerIcon = value.ColorStringValue;
                break;
            case ThemePaletteColor.DrawerBackground:
                newPalette.DrawerBackground = value.ColorStringValue;
                break;
            case ThemePaletteColor.AppbarText:
                newPalette.AppbarText = value.ColorStringValue;
                break;
            case ThemePaletteColor.AppbarBackground:
                newPalette.AppbarBackground = value.ColorStringValue;
                break;
            case ThemePaletteColor.LinesDefault:
                newPalette.LinesDefault = value.ColorStringValue;
                break;
            case ThemePaletteColor.LinesInputs:
                newPalette.LinesInputs = value.ColorStringValue;
                break;
            case ThemePaletteColor.Divider:
                newPalette.Divider = value.ColorStringValue;
                break;
            case ThemePaletteColor.DividerLight:
                newPalette.DividerLight = value.ColorStringValue;
                break;
            case ThemePaletteColor.TextPrimary:
                newPalette.TextPrimary = value.ColorStringValue;
                break;
            case ThemePaletteColor.TextSecondary:
                newPalette.TextSecondary = value.ColorStringValue;
                break;
            case ThemePaletteColor.TextDisabled:
                newPalette.TextDisabled = value.ColorStringValue;
                break;
            case ThemePaletteColor.ActionDefault:
                newPalette.ActionDefault = value.ColorStringValue;
                break;
            case ThemePaletteColor.ActionDisabled:
                newPalette.ActionDisabled = value.ColorStringValue;
                break;
            case ThemePaletteColor.ActionDisabledBackground:
                newPalette.ActionDisabledBackground = value.ColorStringValue;
                break;
        }

        if (_isDarkModeState.Value)
        {
            _customTheme.PaletteDark = (PaletteDark)newPalette;
        }
        else
        {
            _customTheme.PaletteLight = (PaletteLight)newPalette;
        }
        if (_isDarkModeState.Value)
        {
            _currentPaletteDark = _customTheme.PaletteDark;
            Theme.Theme.PaletteDark = _customTheme.PaletteDark;
        }
        else
        {
            _currentPaletteLight = _customTheme.PaletteLight;
            Theme.Theme.PaletteLight = _customTheme.PaletteLight;
        }

        return UpdateThemeChangedAsync();
    }

    /// <summary>
    /// Updates the Open value asynchronously.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task UpdateOpenValueAsync() => _openState.SetValueAsync(false);

    /// <summary>
    /// Updates the theme changed asynchronously.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual async Task UpdateThemeChangedAsync()
    {
        await ThemeChanged.InvokeAsync(Theme);
        StateHasChanged();
    }

    /// <summary>
    /// Handles the parameter changed event for the IsDarkMode property.
    /// </summary>
    /// <param name="arg">The parameter changed event arguments.</param>
    protected virtual void OnIsDarkModeChanged(ParameterChangedEventArgs<bool> arg)
    {
        if (_customTheme is not null)
        {
            UpdateCustomTheme();
        }
    }

    /// <summary>
    /// Handles the DrawerClipMode event asynchronously.
    /// </summary>
    /// <param name="value">The drawer clip mode value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnDrawerClipModeAsync(DrawerClipMode value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        Theme.DrawerClipMode = value;

        return UpdateThemeChangedAsync();
    }

    /// <summary>
    /// Handles the DefaultBorderRadius event asynchronously.
    /// </summary>
    /// <param name="value">The default border radius value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnDefaultBorderRadiusAsync(int value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        if (_customTheme is null)
        {
            return Task.CompletedTask;
        }

        Theme.DefaultBorderRadius = value;
        var newBorderRadius = _customTheme.LayoutProperties;

        newBorderRadius.DefaultBorderRadius = $"{value}px";

        _customTheme.LayoutProperties = newBorderRadius;
        Theme.Theme = _customTheme;

        return UpdateThemeChangedAsync();
    }

    /// <summary>
    /// Handles the DefaultElevation event asynchronously.
    /// </summary>
    /// <param name="value">The default elevation value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnDefaultElevationAsync(int value)
    {
        if (Theme is null || _customTheme is null)
        {
            return Task.CompletedTask;
        }

        Theme.DefaultElevation = value;
        var newDefaultElevation = _customTheme.Shadows;

        string newElevation = newDefaultElevation.Elevation[value];
        newDefaultElevation.Elevation[1] = newElevation;

        _customTheme.Shadows.Elevation[1] = newElevation;
        Theme.Theme = _customTheme;

        return UpdateThemeChangedAsync();
    }

    /// <summary>
    /// Handles the AppBarElevation event asynchronously.
    /// </summary>
    /// <param name="value">The app bar elevation value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnAppBarElevationAsync(int value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        Theme.AppBarElevation = value;

        return UpdateThemeChangedAsync();
    }

    /// <summary>
    /// Handles the DrawerElevation event asynchronously.
    /// </summary>
    /// <param name="value">The drawer elevation value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnDrawerElevationAsync(int value)
    {
        if (Theme is null)
        {
            return Task.CompletedTask;
        }

        Theme.DrawerElevation = value;

        return UpdateThemeChangedAsync();
    }

    /// <summary>
    /// Handles the FontFamily event asynchronously.
    /// </summary>
    /// <param name="value">The font family value.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnFontFamilyAsync(string value)
    {
        if (Theme is null || _customTheme is null)
        {
            return Task.CompletedTask;
        }

        Theme.FontFamily = value;
        var newTypography = _customTheme.Typography;

        newTypography.Body1.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Body2.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Button.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Caption.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Default.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.H1.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.H2.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.H3.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.H4.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.H5.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.H6.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Overline.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Subtitle1.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };
        newTypography.Subtitle2.FontFamily = new[] { value, "Helvetica", "Arial", "sans-serif" };

        _customTheme.Typography = newTypography;
        Theme.Theme = _customTheme;

        return UpdateThemeChangedAsync();
    }

    private void UpdateCustomTheme()
    {
        if (_customTheme is null)
        {
            return;
        }

        if (_currentPaletteLight is not null)
        {
            _customTheme.PaletteLight = _currentPaletteLight;
        }

        if (_currentPaletteDark is not null)
        {
            _customTheme.PaletteDark = _currentPaletteDark;
        }

        _currentPalette = GetPalette();
    }

    private Palette GetPalette() => _isDarkModeState.Value
        ? _currentPaletteDark ?? DefaultPaletteDark
        : _currentPaletteLight ?? DefaultPaletteLight;
}
