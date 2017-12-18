using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            this.DataContext = _mainViewModel;

            _timer.Interval = new TimeSpan(2000);
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
                    EntireView();
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
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.Topmost = true;
                //this.IsTopmost = true;
                //this.CanClose = false;
                //this.CanMove = false;

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
                this.ResizeMode = ResizeMode.CanResize;
                //this.CanClose = true;
                //this.CanMove = true;

                this.Left = 100;
                this.Top = 100;
                this.Width = 800;
                this.Height = 600;

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
            Dictionary<Prize, Person> drawList = new Dictionary<Prize, Person>();

            var prize1 = _mainViewModel.RandomPrize;
            var person1 = _mainViewModel.RandomPerson;
            if (prize1 != null && person1 != null)
            {
                drawList.Add(prize1, person1);
                radCarousePrizel.BringDataItemIntoView(prize1);
                radCarousePerson1.BringDataItemIntoView(person1);
            }
            else
            {
                radCarousePrizel.Visibility = Visibility.Collapsed;
                radCarousePerson1.Visibility = Visibility.Collapsed;
            }

            var prize2 = _mainViewModel.RandomPrize;
            var person2 = _mainViewModel.RandomPerson;
            if (prize2 != null && person2 != null)
            {
                drawList.Add(prize2, person2);
                radCarousePrize2.BringDataItemIntoView(prize2);
                radCarousePerson2.BringDataItemIntoView(person2);
            }
            else
            {
                radCarousePrize2.Visibility = Visibility.Collapsed;
                radCarousePerson2.Visibility = Visibility.Collapsed;
            }

            var prize3 = _mainViewModel.RandomPrize;
            var person3 = _mainViewModel.RandomPerson;
            if (prize3 != null && person3 != null)
            {
                drawList.Add(prize3, person3);
                radCarousePrize3.BringDataItemIntoView(prize3);
                radCarousePerson3.BringDataItemIntoView(person3);
            }
            else
            {
                radCarousePrize3.Visibility = Visibility.Collapsed;
                radCarousePerson3.Visibility = Visibility.Collapsed;
            }

            var prize4 = _mainViewModel.RandomPrize;
            var person4 = _mainViewModel.RandomPerson;
            if (prize4 != null && person4 != null)
            {
                drawList.Add(prize4, person4);
                radCarousePrizel.BringDataItemIntoView(prize4);
                radCarousePerson1.BringDataItemIntoView(person4);
            }
            else
            {
                radCarousePrize4.Visibility = Visibility.Collapsed;
                radCarousePerson4.Visibility = Visibility.Collapsed;
            }

            var prize5 = _mainViewModel.RandomPrize;
            var person5 = _mainViewModel.RandomPerson;
            if (prize5 != null && person5 != null)
            {
                drawList.Add(prize5, person5);
                radCarousePrize5.BringDataItemIntoView(prize5);
                radCarousePerson5.BringDataItemIntoView(person5);
            }
            else
            {
                radCarousePrize5.Visibility = Visibility.Collapsed;
                radCarousePerson5.Visibility = Visibility.Collapsed;
            }

            if (!_mainViewModel.WriteData(drawList))
            {
                foreach(var i in drawList)
                {
                    i.Key.IsUsed = false;
                    i.Value.IsUsed = false;
                }
            }
        }
    }
}
