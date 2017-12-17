using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace LuckyDraw
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RadWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            this.DataContext = _mainViewModel;

            _timer.Interval = new TimeSpan(1000);
            _timer.Tick += Timer_Tick;
        }

        //ViewModel，存储界面信息及读写操作
        private MainViewModel _mainViewModel;

        private bool _isEntrieView = false; //全屏状态
        private bool _isDrawing = false; //抽奖状态
        DispatcherTimer _timer = new DispatcherTimer(); //屏幕滚动定时器
        Random _random = new Random(); //随机数生成器

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                EntireView();
            }
            catch (Exception ex) { }

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    this.WindowState = WindowState.Normal;
                    //this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.CanClose = true;
                    this.CanMove = true;

                    double left = ActualWidth / 2 - 200;
                    double top = ActualHeight / 2 - 200;
                    this.Left = left > 0 ? left : 0;
                    this.Top = top > 0 ? top : 0;
                    this.Width = 400;
                    this.Height = 400;

                    _isEntrieView = false;
                }
                else if (e.Key == Key.Space)
                {
                    if (_isDrawing)
                    {
                        //停止滚动
                        _timer.Stop();

                        GetDrawResult();
                    }
                    else
                    {
                        //去除已经抽过的
                        _mainViewModel.RefrehList();

                        //判断奖品数和抽奖人数目
                        if (_mainViewModel.PrizeCount == 0)
                        {
                            MessageBox.Show("奖品已抽完！");
                            return;
                        }

                        if (_mainViewModel.PeopleCount == 0)
                        {
                            MessageBox.Show("抽奖人不足！");
                        }

                        //开始滚动
                        _timer.Start();
                    }

                    _isDrawing = !_isDrawing;
                }
            }
            catch (Exception ex) { }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EntireView();
            }
            catch (Exception ex) { }
        }

        private void EntireView()
        {
            if (!_isEntrieView)
            {
                //设置全屏
                this.WindowState = WindowState.Normal;
                //this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                //this.Topmost = true;
                this.IsTopmost = true;
                this.CanClose = false;
                this.CanMove = false;

                this.Left = 0;
                this.Top = 0;
                this.Width = SystemParameters.PrimaryScreenWidth;
                this.Height = SystemParameters.PrimaryScreenHeight;

                _isEntrieView = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                //this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.CanClose = true;
                this.CanMove = true;

                this.Left = ActualWidth / 2 - 200;
                this.Top = ActualHeight / 2 - 200;
                this.Width = 400;
                this.Height = 400;

                _isEntrieView = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int rad = _random.Next(0, radCarousePrizel.Items.Count - 1);
                radCarousePrizel.BringDataItemIntoView(radCarousePrizel.Items[rad]);

                rad = _random.Next(0, radCarousePrize2.Items.Count - 1);
                radCarousePrize2.BringDataItemIntoView(radCarousePrize2.Items[rad]);

                rad = _random.Next(0, radCarousePrize3.Items.Count - 1);
                radCarousePrize3.BringDataItemIntoView(radCarousePrize3.Items[rad]);

                rad = _random.Next(0, radCarousePrize4.Items.Count - 1);
                radCarousePrize4.BringDataItemIntoView(radCarousePrize4.Items[rad]);

                rad = _random.Next(0, radCarousePrize5.Items.Count - 1);
                radCarousePrize5.BringDataItemIntoView(radCarousePrize5.Items[rad]);

                rad = _random.Next(0, radCarousePerson1.Items.Count - 1);
                radCarousePerson1.BringDataItemIntoView(radCarousePerson1.Items[rad]);

                rad = _random.Next(0, radCarousePerson2.Items.Count - 1);
                radCarousePerson2.BringDataItemIntoView(radCarousePerson2.Items[rad]);

                rad = _random.Next(0, radCarousePerson3.Items.Count - 1);
                radCarousePerson3.BringDataItemIntoView(radCarousePerson3.Items[rad]);

                rad = _random.Next(0, radCarousePerson4.Items.Count - 1);
                radCarousePerson4.BringDataItemIntoView(radCarousePerson4.Items[rad]);

                rad = _random.Next(0, radCarousePerson5.Items.Count - 1);
                radCarousePerson5.BringDataItemIntoView(radCarousePerson5.Items[rad]);
            }
            catch (Exception ex) { }
        }

        //获取抽奖结果
        private void GetDrawResult()
        {
            //todo:随机产生10个不重复的数字
            int rad = _random.Next(0, radCarousePrizel.Items.Count - 1);
            var prize1 = radCarousePrizel.Items[rad] as Prize;
            if (prize1 != null)
            {
                radCarousePrizel.BringDataItemIntoView(radCarousePrizel.Items[rad]);
                prize1.IsUsed = true;
            }

            var prize2 = radCarousePrize2.Items[rad] as Prize;
            if (prize2 != null)
            {
                radCarousePrize2.BringDataItemIntoView(radCarousePrize2.Items[rad]);
                prize2.IsUsed = true;
            }

            var prize3 = radCarousePrize3.Items[rad] as Prize;
            if (prize3 != null)
            {
                radCarousePrize3.BringDataItemIntoView(radCarousePrize3.Items[rad]);
                prize3.IsUsed = true;
            }

            var prize4 = radCarousePrize4.Items[rad] as Prize;
            if (prize4 != null)
            {
                radCarousePrize4.BringDataItemIntoView(radCarousePrize4.Items[rad]);
                prize4.IsUsed = true;
            }

            var prize5 = radCarousePrize5.Items[rad] as Prize;
            if (prize5 != null)
            {
                radCarousePrize5.BringDataItemIntoView(radCarousePrize5.Items[rad]);
                prize5.IsUsed = true;
            }

            //todo:添加其他
        }
    }
}
