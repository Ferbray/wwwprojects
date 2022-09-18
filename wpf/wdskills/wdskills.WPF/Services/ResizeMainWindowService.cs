using System;
namespace wdskills.WPF.Services
{
    public class ResizeMainWindowService
    {
        public SizeMainWindow? SizeWindow { get; private set; }
        public event Action<SizeMainWindow>? OnResizeWindowChanged;
        public void ChangeSizeMainWindow(SizeMainWindow? resizeWindow)
        {
            SizeWindow = resizeWindow;
            OnResizeWindowChanged?.Invoke(SizeWindow ?? new(8000, 8000, 450, 450));
        }
    }

    public class SizeMainWindow
    {
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MinWidth { get; set; }
        public int MinHeight { get; set; }

        public SizeMainWindow(
            int maxWidth,
            int maxHeight,
            int minWidth,
            int minHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            MinWidth = minWidth;
            MinHeight = minHeight;
        }
    }
}
