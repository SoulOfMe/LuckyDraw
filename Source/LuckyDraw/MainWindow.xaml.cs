using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace LuckyDraw
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private bool _isEntrieView = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EntireView();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;

                double left = ActualWidth / 2 - 200;
                double top = ActualHeight / 2 - 200;
                this.Left = left > 0 ? left : 0;
                this.Top = top > 0 ? top : 0;
                this.Width = 400;
                this.Height = 400;

                _isEntrieView = false;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EntireView();
        }

        private void EntireView()
        {
            if (!_isEntrieView)
            {
                //设置全屏
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.Topmost = true;

                this.Left = 0;
                this.Top = 0;
                this.Width = SystemParameters.PrimaryScreenWidth;
                this.Height = SystemParameters.PrimaryScreenHeight;

                _isEntrieView = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;

                this.Left = ActualWidth / 2 - 200;
                this.Top = ActualHeight / 2 - 200;
                this.Width = 400;
                this.Height = 400;

                _isEntrieView = false;
            }
        }
    }
}
