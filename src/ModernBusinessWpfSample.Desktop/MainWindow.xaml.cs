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
    private bool _isDarkMode;

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
            SetResourceColor("AppBg", Color.FromRgb(15, 23, 42));
            SetResourceColor("Surface", Color.FromRgb(17, 24, 39));
            SetResourceColor("SurfaceAlt", Color.FromRgb(30, 41, 59));
            SetResourceColor("BorderSoft", Color.FromRgb(51, 65, 85));
            SetResourceColor("TextStrong", Color.FromRgb(248, 250, 252));
            SetResourceColor("TextMuted", Color.FromRgb(203, 213, 225));
            SetResourceColor("Accent", Color.FromRgb(37, 99, 235));
            SetResourceColor("AccentHover", Color.FromRgb(29, 78, 216));
            SetResourceColor("AccentText", Color.FromRgb(191, 219, 254));
            SetResourceColor("AccentSoft", Color.FromRgb(30, 58, 138));
            SetResourceColor("SuccessSoft", Color.FromRgb(6, 78, 59));
            SetResourceColor("Success", Color.FromRgb(52, 211, 153));
            SetResourceColor("TitleBarBg", Color.FromRgb(2, 6, 23));
            SetResourceColor("TitleBarBg2", Color.FromRgb(30, 64, 175));
            SetResourceColor("TitleBarBorder", Color.FromRgb(59, 130, 246));
            SetResourceColor("TitleBarText", Color.FromRgb(248, 250, 252));
            SetResourceColor("TitleBarSubtle", Color.FromRgb(191, 219, 254));
            SetResourceColor("NavigationHover", Color.FromRgb(30, 41, 59));
            SetResourceColor("RowBorder", Color.FromRgb(51, 65, 85));
            SetResourceColor("HeaderIconBg", Color.FromRgb(30, 58, 138));
            SetResourceColor("HeaderBadgeBg", Color.FromRgb(30, 64, 175));
            SetResourceColor("HeaderUserBg", Color.FromRgb(30, 41, 59));
            SetResourceColor("HeaderBadgeBorder", Color.FromRgb(96, 165, 250));
            SetResourceColor("HeaderOnline", Color.FromRgb(56, 189, 248));
            SetResourceColor("HeaderOnlineText", Color.FromRgb(219, 234, 254));

            TitleGradientStart.Color = Color.FromRgb(2, 6, 23);
            TitleGradientEnd.Color = Color.FromRgb(30, 64, 175);
            ThemeToggleButton.Content = "ライトモードに切替";
            StatusMessageIfPossible("ダークモードに切り替えました。");
        }
        else
        {
            SetResourceColor("AppBg", Color.FromRgb(245, 247, 251));
            SetResourceColor("Surface", Color.FromRgb(255, 255, 255));
            SetResourceColor("SurfaceAlt", Color.FromRgb(248, 250, 252));
            SetResourceColor("BorderSoft", Color.FromRgb(203, 213, 225));
            SetResourceColor("TextStrong", Color.FromRgb(15, 23, 42));
            SetResourceColor("TextMuted", Color.FromRgb(100, 116, 139));
            SetResourceColor("Accent", Color.FromRgb(37, 99, 235));
            SetResourceColor("AccentHover", Color.FromRgb(29, 78, 216));
            SetResourceColor("AccentText", Color.FromRgb(29, 78, 216));
            SetResourceColor("AccentSoft", Color.FromRgb(219, 234, 254));
            SetResourceColor("SuccessSoft", Color.FromRgb(220, 252, 231));
            SetResourceColor("Success", Color.FromRgb(5, 150, 105));
            SetResourceColor("TitleBarBg", Color.FromRgb(15, 23, 42));
            SetResourceColor("TitleBarBg2", Color.FromRgb(37, 99, 235));
            SetResourceColor("TitleBarBorder", Color.FromRgb(29, 78, 216));
            SetResourceColor("TitleBarText", Color.FromRgb(255, 255, 255));
            SetResourceColor("TitleBarSubtle", Color.FromRgb(219, 234, 254));
            SetResourceColor("NavigationHover", Color.FromRgb(239, 246, 255));
            SetResourceColor("RowBorder", Color.FromRgb(226, 232, 240));
            SetResourceColor("HeaderIconBg", Color.FromRgb(239, 246, 255));
            SetResourceColor("HeaderBadgeBg", Color.FromRgb(29, 78, 216));
            SetResourceColor("HeaderUserBg", Color.FromRgb(30, 58, 138));
            SetResourceColor("HeaderBadgeBorder", Color.FromRgb(96, 165, 250));
            SetResourceColor("HeaderOnline", Color.FromRgb(96, 165, 250));
            SetResourceColor("HeaderOnlineText", Color.FromRgb(219, 234, 254));

            TitleGradientStart.Color = Color.FromRgb(15, 23, 42);
            TitleGradientEnd.Color = Color.FromRgb(37, 99, 235);
            ThemeToggleButton.Content = "ダークモードに切替";
            StatusMessageIfPossible("ライトモードに切り替えました。");
        }

        ApplyNativeTitleBarColors();
    }

    private void SetResourceColor(string resourceKey, Color color)
    {
        Resources[resourceKey] = new SolidColorBrush(color);
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
            ? ToColorRef(59, 130, 246)
            : ToColorRef(29, 78, 216);

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
