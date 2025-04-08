using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Wpf;

namespace FkgNewsViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static string SourceUrl() => $"https://web.flower-knight-girls.co.jp/web/news/news?date={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}&pft=2";

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new { SourceUrl = SourceUrl() };
#if (!DEBUG)
            WebView.CoreWebView2InitializationCompleted += (sender, args) =>
            {
                if (args.IsSuccess)
                {
                    var wv2 = (WebView2) sender;
                    wv2.CoreWebView2.Settings.AreDevToolsEnabled = false;
                }
            };
#endif
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new { SourceUrl = SourceUrl() };
            WebView.CoreWebView2.Reload();
        }

        private async void Transit_Click(object sender, RoutedEventArgs e)
        {
            var maxPage = await WebView.CoreWebView2.ExecuteScriptAsync(Scripts.GetMenuMaxPage);

            var ok = int.TryParse(maxPage.Trim('"'), out var page);

            var dialog = new PageTransit(page);
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            if (int.TryParse(dialog.UserInput, out var ui) && (!ok || (ui > 0 && ui <= page)))
            {
                await WebView.CoreWebView2.ExecuteScriptAsync($@"var page = {ui};var maxPage={page};");
                await WebView.CoreWebView2.ExecuteScriptAsync(Scripts.ChangeMenuUrl);
            }
            else
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("WIN_PAGE_MAIN_INVALID_INT", Properties.Resources.Culture) + $"1 - {page}");
            }
        }

        private async void Print_Click(object sender, RoutedEventArgs e)
        {
            await WebView.CoreWebView2.ExecuteScriptAsync(Scripts.PrintArticle);
        }
    }
}
