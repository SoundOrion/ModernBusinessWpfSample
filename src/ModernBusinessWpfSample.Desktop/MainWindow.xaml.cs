using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using ModernBusinessWpfSample.Desktop.ViewModels;

namespace ModernBusinessWpfSample.Desktop;

public partial class MainWindow : Window
{
    private bool _isDarkMode = true;

    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        SourceInitialized += (_, _) => ApplyNativeTitleBarColors();
        StateChanged += (_, _) => UpdateMaximizeRestoreButton();
        UpdateMaximizeRestoreButton();
    }

    private void AppHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left)
        {
            return;
        }

        // Do not start window dragging when clicking header actions such as the window buttons.
        if (e.OriginalSource is DependencyObject source && FindAncestor<Button>(source) is not null)
        {
            return;
        }

        if (e.ClickCount == 2)
        {
            ToggleMaximizeRestore();
            return;
        }

        try
        {
            DragMove();
        }
        catch (InvalidOperationException)
        {
            // DragMove can throw if the mouse button state changes during the event.
        }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeRestoreButton_Click(object sender, RoutedEventArgs e)
    {
        ToggleMaximizeRestore();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
    {
        _isDarkMode = !_isDarkMode;
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        if (_isDarkMode)
        {
            SetResourceColor("AppBg", Color.FromRgb(7, 10, 18));
            SetResourceColor("Surface", Color.FromArgb(230, 26, 32, 51));
            SetResourceColor("SurfaceAlt", Color.FromArgb(184, 20, 26, 42));
            SetResourceColor("BorderSoft", Color.FromArgb(51, 255, 255, 255));
            SetResourceColor("TextStrong", Color.FromRgb(248, 250, 252));
            SetResourceColor("TextMuted", Color.FromRgb(154, 168, 199));
            SetResourceColor("Accent", Color.FromRgb(124, 58, 237));
            SetResourceColor("AccentHover", Color.FromRgb(6, 182, 212));
            SetResourceColor("AccentSoft", Color.FromArgb(38, 35, 91, 255));
            SetResourceColor("SuccessSoft", Color.FromArgb(30, 16, 185, 129));
            SetResourceColor("Success", Color.FromRgb(45, 212, 191));
            SetResourceColor("TitleBarBg", Color.FromRgb(7, 10, 18));
            SetResourceColor("TitleBarBg2", Color.FromRgb(17, 24, 39));
            SetResourceColor("TitleBarBorder", Color.FromRgb(51, 65, 85));
            SetResourceColor("TitleBarText", Color.FromRgb(255, 255, 255));
            SetResourceColor("TitleBarSubtle", Color.FromRgb(165, 180, 252));
            SetResourceColor("NavigationHover", Color.FromArgb(38, 255, 255, 255));
            SetResourceColor("RowBorder", Color.FromRgb(31, 41, 55));
            SetResourceColor("HeaderIconBg", Color.FromArgb(46, 30, 27, 75));
            SetResourceColor("HeaderBadgeBg", Color.FromArgb(43, 15, 23, 42));
            SetResourceColor("HeaderUserBg", Color.FromArgb(51, 30, 41, 59));
            SetResourceColor("HeaderBadgeBorder", Color.FromArgb(102, 124, 58, 237));
            SetResourceColor("HeaderOnline", Color.FromRgb(52, 211, 153));
            SetResourceColor("HeaderOnlineText", Color.FromRgb(209, 250, 229));
            SetResourceColor("Danger", Color.FromRgb(251, 113, 133));
            SetResourceColor("Warning", Color.FromRgb(251, 191, 36));
            SetResourceColor("GlassStroke", Color.FromArgb(64, 255, 255, 255));
            SetResourceColor("InputFill", Color.FromArgb(20, 255, 255, 255));

            SetTitleGradientColors(Color.FromRgb(49, 46, 129), Color.FromRgb(6, 182, 212));
            ThemeToggleButton.Content = "ライトモードに切替";
            StatusMessageIfPossible("2026ダークモードに切り替えました。");
        }
        else
        {
            SetResourceColor("AppBg", Color.FromRgb(239, 244, 255));
            SetResourceColor("Surface", Color.FromArgb(232, 255, 255, 255));
            SetResourceColor("SurfaceAlt", Color.FromArgb(205, 248, 250, 252));
            SetResourceColor("BorderSoft", Color.FromArgb(140, 148, 163, 184));
            SetResourceColor("TextStrong", Color.FromRgb(15, 23, 42));
            SetResourceColor("TextMuted", Color.FromRgb(71, 85, 105));
            SetResourceColor("Accent", Color.FromRgb(99, 102, 241));
            SetResourceColor("AccentHover", Color.FromRgb(8, 145, 178));
            SetResourceColor("AccentSoft", Color.FromArgb(70, 224, 231, 255));
            SetResourceColor("SuccessSoft", Color.FromArgb(70, 209, 250, 229));
            SetResourceColor("Success", Color.FromRgb(13, 148, 136));
            SetResourceColor("TitleBarBg", Color.FromRgb(255, 255, 255));
            SetResourceColor("TitleBarBg2", Color.FromRgb(238, 242, 255));
            SetResourceColor("TitleBarBorder", Color.FromRgb(199, 210, 254));
            SetResourceColor("TitleBarText", Color.FromRgb(15, 23, 42));
            SetResourceColor("TitleBarSubtle", Color.FromRgb(79, 70, 229));
            SetResourceColor("NavigationHover", Color.FromArgb(150, 255, 255, 255));
            SetResourceColor("RowBorder", Color.FromRgb(226, 232, 240));
            SetResourceColor("HeaderIconBg", Color.FromArgb(95, 224, 231, 255));
            SetResourceColor("HeaderBadgeBg", Color.FromArgb(150, 255, 255, 255));
            SetResourceColor("HeaderUserBg", Color.FromArgb(120, 238, 242, 255));
            SetResourceColor("HeaderBadgeBorder", Color.FromArgb(150, 99, 102, 241));
            SetResourceColor("HeaderOnline", Color.FromRgb(13, 148, 136));
            SetResourceColor("HeaderOnlineText", Color.FromRgb(4, 47, 46));
            SetResourceColor("Danger", Color.FromRgb(225, 29, 72));
            SetResourceColor("Warning", Color.FromRgb(217, 119, 6));
            SetResourceColor("GlassStroke", Color.FromArgb(175, 255, 255, 255));
            SetResourceColor("InputFill", Color.FromArgb(185, 255, 255, 255));

            SetTitleGradientColors(Color.FromRgb(79, 70, 229), Color.FromRgb(14, 165, 233));
            ThemeToggleButton.Content = "ダークモードに切替";
            StatusMessageIfPossible("クリーンライトモードに切り替えました。");
        }

        ApplyNativeTitleBarColors();
    }

    private void SetResourceColor(string resourceKey, Color color)
    {
        Resources[resourceKey] = new SolidColorBrush(color);
    }

    private void SetTitleGradientColors(Color startColor, Color endColor)
    {
        if (Resources["TitleGradientBrush"] is not LinearGradientBrush titleGradientBrush ||
            titleGradientBrush.GradientStops.Count < 3)
        {
            return;
        }

        titleGradientBrush.GradientStops[0].Color = startColor;
        titleGradientBrush.GradientStops[2].Color = endColor;
    }

    private void StatusMessageIfPossible(string message)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.StatusMessage = message;
        }
    }

    private void ToggleMaximizeRestore()
    {
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
    }

    private void UpdateMaximizeRestoreButton()
    {
        if (MaximizeRestoreButton is null)
        {
            return;
        }

        MaximizeRestoreButton.Content = WindowState == WindowState.Maximized ? "❐" : "□";
        MaximizeRestoreButton.ToolTip = WindowState == WindowState.Maximized ? "元に戻す" : "最大化";
    }

    private static T? FindAncestor<T>(DependencyObject current) where T : DependencyObject
    {
        while (current is not null)
        {
            if (current is T match)
            {
                return match;
            }

            current = VisualTreeHelper.GetParent(current);
        }

        return null;
    }

    private void ApplyNativeTitleBarColors()
    {
        if (!OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000))
        {
            return;
        }

        var hwnd = new WindowInteropHelper(this).Handle;

        // COLORREF format: 0x00BBGGRR
        var captionColor = _isDarkMode
            ? ToColorRef(2, 6, 23)
            : ToColorRef(15, 23, 42);
        var textColor = ToColorRef(255, 255, 255);
        var borderColor = _isDarkMode
            ? ToColorRef(51, 65, 85)
            : ToColorRef(30, 58, 138);

        _ = DwmSetWindowAttribute(hwnd, DWMWA_CAPTION_COLOR, ref captionColor, sizeof(int));
        _ = DwmSetWindowAttribute(hwnd, DWMWA_TEXT_COLOR, ref textColor, sizeof(int));
        _ = DwmSetWindowAttribute(hwnd, DWMWA_BORDER_COLOR, ref borderColor, sizeof(int));
    }

    private static int ToColorRef(byte r, byte g, byte b) => r | (g << 8) | (b << 16);

    private const int DWMWA_BORDER_COLOR = 34;
    private const int DWMWA_CAPTION_COLOR = 35;
    private const int DWMWA_TEXT_COLOR = 36;

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(
        IntPtr hwnd,
        int dwAttribute,
        ref int pvAttribute,
        int cbAttribute);
}
