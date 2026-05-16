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
            SetResourceColor("AppBg", Color.FromRgb(5, 35, 25));
            SetResourceColor("Surface", Color.FromRgb(8, 54, 38));
            SetResourceColor("SurfaceAlt", Color.FromRgb(3, 43, 29));
            SetResourceColor("BorderSoft", Color.FromRgb(23, 91, 61));
            SetResourceColor("TextStrong", Color.FromRgb(240, 253, 244));
            SetResourceColor("TextMuted", Color.FromRgb(163, 201, 178));
            SetResourceColor("Accent", Color.FromRgb(52, 211, 153));
            SetResourceColor("AccentHover", Color.FromRgb(47, 191, 106));
            SetResourceColor("AccentSoft", Color.FromRgb(0, 107, 49));
            SetResourceColor("SuccessSoft", Color.FromRgb(6, 78, 43));
            SetResourceColor("Success", Color.FromRgb(52, 211, 153));
            SetResourceColor("TitleBarBg", Color.FromRgb(0, 48, 32));
            SetResourceColor("TitleBarBg2", Color.FromRgb(8, 54, 38));
            SetResourceColor("TitleBarBorder", Color.FromRgb(23, 91, 61));
            SetResourceColor("TitleBarText", Color.FromRgb(247, 251, 248));
            SetResourceColor("TitleBarSubtle", Color.FromRgb(167, 243, 208));
            SetResourceColor("NavigationHover", Color.FromRgb(8, 54, 38));
            SetResourceColor("RowBorder", Color.FromRgb(23, 91, 61));
            SetResourceColor("HeaderIconBg", Color.FromRgb(0, 107, 49));
            SetResourceColor("HeaderBadgeBg", Color.FromRgb(8, 54, 38));
            SetResourceColor("HeaderUserBg", Color.FromRgb(5, 35, 25));
            SetResourceColor("HeaderBadgeBorder", Color.FromRgb(23, 91, 61));
            SetResourceColor("HeaderOnline", Color.FromRgb(52, 211, 153));
            SetResourceColor("HeaderOnlineText", Color.FromRgb(220, 252, 231));

            TitleGradientStart.Color = Color.FromRgb(0, 48, 32);
            TitleGradientEnd.Color = Color.FromRgb(8, 54, 38);
            ThemeToggleButton.Content = "ライトモードに切替";
            StatusMessageIfPossible("ダークモードに切り替えました。");
        }
        else
        {
            SetResourceColor("AppBg", Color.FromRgb(241, 248, 244));
            SetResourceColor("Surface", Color.FromRgb(255, 255, 255));
            SetResourceColor("SurfaceAlt", Color.FromRgb(247, 251, 248));
            SetResourceColor("BorderSoft", Color.FromRgb(214, 233, 221));
            SetResourceColor("TextStrong", Color.FromRgb(18, 51, 38));
            SetResourceColor("TextMuted", Color.FromRgb(93, 119, 107));
            SetResourceColor("Accent", Color.FromRgb(0, 132, 61));
            SetResourceColor("AccentHover", Color.FromRgb(0, 107, 49));
            SetResourceColor("AccentSoft", Color.FromRgb(229, 244, 235));
            SetResourceColor("SuccessSoft", Color.FromRgb(230, 247, 237));
            SetResourceColor("Success", Color.FromRgb(0, 154, 68));
            SetResourceColor("TitleBarBg", Color.FromRgb(0, 72, 49));
            SetResourceColor("TitleBarBg2", Color.FromRgb(0, 132, 61));
            SetResourceColor("TitleBarBorder", Color.FromRgb(0, 107, 49));
            SetResourceColor("TitleBarText", Color.FromRgb(255, 255, 255));
            SetResourceColor("TitleBarSubtle", Color.FromRgb(189, 239, 209));
            SetResourceColor("NavigationHover", Color.FromRgb(240, 253, 244));
            SetResourceColor("RowBorder", Color.FromRgb(228, 241, 233));
            SetResourceColor("HeaderIconBg", Color.FromRgb(216, 243, 227));
            SetResourceColor("HeaderBadgeBg", Color.FromRgb(0, 107, 49));
            SetResourceColor("HeaderUserBg", Color.FromRgb(0, 82, 58));
            SetResourceColor("HeaderBadgeBorder", Color.FromRgb(47, 191, 106));
            SetResourceColor("HeaderOnline", Color.FromRgb(47, 191, 106));
            SetResourceColor("HeaderOnlineText", Color.FromRgb(220, 252, 231));

            TitleGradientStart.Color = Color.FromRgb(0, 72, 49);
            TitleGradientEnd.Color = Color.FromRgb(0, 132, 61);
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
            ? ToColorRef(0, 48, 32)
            : ToColorRef(0, 72, 49);
        var textColor = ToColorRef(255, 255, 255);
        var borderColor = _isDarkMode
            ? ToColorRef(23, 91, 61)
            : ToColorRef(0, 132, 61);

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
