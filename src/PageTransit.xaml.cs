using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace FkgNewsViewer
{
    public partial class PageTransit : Window
    {
        public string UserInput { get; private set; }

        // winuser.h
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        // winuser.h
        [DllImport("user32.dll")]
        private static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        // ReSharper disable once InconsistentNaming
        private const uint SC_MAXIMIZE = 0xF030;

        // ReSharper disable once InconsistentNaming
        private const uint SC_MINIMIZE = 0xF020;

        // ReSharper disable once InconsistentNaming
        private const uint MF_BYCOMMAND = 0x000000000;

        public PageTransit(int maxPage)
        {
            InitializeComponent();
            MaxPage.Text = maxPage > 0 ? maxPage.ToString() : Properties.Resources.ResourceManager.GetString("WIN_PAGE_TRANSIT_NULL", Properties.Resources.Culture);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper((Window) sender).Handle;
            var hMenu = GetSystemMenu(hwnd, false);
            RemoveMenu(hMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            RemoveMenu(hMenu, SC_MINIMIZE, MF_BYCOMMAND);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            InputTextBox.SelectAll();
            InputTextBox.Focus();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            UserInput = InputTextBox.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
