using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

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
                            return;
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
                //this.Topmost = true;
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
                //奖品
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize1 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize2 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize3 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize4 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize5 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize6 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize7 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize8 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize9 = _mainViewModel.PrizesList[rad].Name;

                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                _mainViewModel.Prize10 = _mainViewModel.PrizesList[rad].Name;

                //人名
                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department1 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person1 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department2 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person2 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department3 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person3 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department4 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person4 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department5 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person5 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department6 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person6 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department7 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person7 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department8 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person8 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department9 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person9 = _mainViewModel.PeopleList[rad].Name;

                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                _mainViewModel.Department10 = _mainViewModel.PeopleList[rad].Department;
                _mainViewModel.Person10 = _mainViewModel.PeopleList[rad].Name;
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
                _mainViewModel.Prize1 = prize1.Name;
                _mainViewModel.Department1 = person1.Department;
                _mainViewModel.Person1 = person1.Name;
                drawList.Add(prize1, person1);
            }
            else
            {
                _mainViewModel.Prize1 = "";
                _mainViewModel.Department1 = "";
                _mainViewModel.Person1 = "";
            }

            var prize2 = _mainViewModel.RandomPrize;
            var person2 = _mainViewModel.RandomPerson;
            if (prize2 != null && person2 != null)
            {
                _mainViewModel.Prize2 = prize2.Name;
                _mainViewModel.Department2 = person2.Department;
                _mainViewModel.Person2 = person2.Name;
                drawList.Add(prize2, person2);
            }
            else
            {
                _mainViewModel.Prize2 = "";
                _mainViewModel.Department2 = "";
                _mainViewModel.Person2 = "";
            }

            var prize3 = _mainViewModel.RandomPrize;
            var person3 = _mainViewModel.RandomPerson;
            if (prize3 != null && person3 != null)
            {
                _mainViewModel.Prize3 = prize3.Name;
                _mainViewModel.Department3 = person3.Department;
                _mainViewModel.Person3 = person3.Name;
                drawList.Add(prize3, person3);
            }
            else
            {
                _mainViewModel.Prize3 = "";
                _mainViewModel.Department3 = "";
                _mainViewModel.Person3 = "";
            }

            var prize4 = _mainViewModel.RandomPrize;
            var person4 = _mainViewModel.RandomPerson;
            if (prize4 != null && person4 != null)
            {
                _mainViewModel.Prize4 = prize4.Name;
                _mainViewModel.Department4 = person4.Department;
                _mainViewModel.Person4 = person4.Name;
                drawList.Add(prize4, person4);
            }
            else
            {
                _mainViewModel.Prize4 = "";
                _mainViewModel.Department4 = person4.Department;
                _mainViewModel.Person4 = "";
            }

            var prize5 = _mainViewModel.RandomPrize;
            var person5 = _mainViewModel.RandomPerson;
            if (prize5 != null && person5 != null)
            {
                _mainViewModel.Prize5 = prize5.Name;
                _mainViewModel.Department5 = person5.Department;
                _mainViewModel.Person5 = person5.Name;
                drawList.Add(prize5, person5);
            }
            else
            {
                _mainViewModel.Prize5 = "";
                _mainViewModel.Department5 = "";
                _mainViewModel.Person5 = "";
            }

            var prize6 = _mainViewModel.RandomPrize;
            var person6 = _mainViewModel.RandomPerson;
            if (prize6 != null && person6 != null)
            {
                _mainViewModel.Prize6 = prize6.Name;
                _mainViewModel.Department6 = person6.Department;
                _mainViewModel.Person6 = person6.Name;
                drawList.Add(prize6, person6);
            }
            else
            {
                _mainViewModel.Prize6 = "";
                _mainViewModel.Department6 = "";
                _mainViewModel.Person6 = "";
            }

            var prize7 = _mainViewModel.RandomPrize;
            var person7 = _mainViewModel.RandomPerson;
            if (prize7 != null && person7 != null)
            {
                _mainViewModel.Prize7 = prize7.Name;
                _mainViewModel.Department7 = person7.Department;
                _mainViewModel.Person7 = person7.Name;
                drawList.Add(prize7, person7);
            }
            else
            {
                _mainViewModel.Prize7 = "";
                _mainViewModel.Department7 = "";
                _mainViewModel.Person7 = "";
            }

            var prize8 = _mainViewModel.RandomPrize;
            var person8 = _mainViewModel.RandomPerson;
            if (prize8 != null && person8 != null)
            {
                _mainViewModel.Prize8 = prize8.Name;
                _mainViewModel.Department8 = person8.Department;
                _mainViewModel.Person8 = person8.Name;
                drawList.Add(prize8, person8);
            }
            else
            {
                _mainViewModel.Prize8 = "";
                _mainViewModel.Department8 = "";
                _mainViewModel.Person8 = "";
            }

            var prize9 = _mainViewModel.RandomPrize;
            var person9 = _mainViewModel.RandomPerson;
            if (prize9 != null && person9 != null)
            {
                _mainViewModel.Prize9 = prize9.Name;
                _mainViewModel.Department9 = person9.Department;
                _mainViewModel.Person9 = person9.Name;
                drawList.Add(prize9, person9);
            }
            else
            {
                _mainViewModel.Prize9 = "";
                _mainViewModel.Department9 = "";
                _mainViewModel.Person9 = "";
            }

            var prize10 = _mainViewModel.RandomPrize;
            var person10 = _mainViewModel.RandomPerson;
            if (prize10 != null && person10 != null)
            {
                _mainViewModel.Prize10 = prize10.Name;
                _mainViewModel.Department10 = person10.Department;
                _mainViewModel.Person10 = person10.Name;
                drawList.Add(prize10, person10);
            }
            else
            {
                _mainViewModel.Prize10 = "";
                _mainViewModel.Department10 = "";
                _mainViewModel.Person10 = "";
            }

            if (!_mainViewModel.WriteData(drawList))
            {
                foreach (var i in drawList)
                {
                    i.Key.IsUsed = false;
                    i.Value.IsUsed = false;
                }
            }
        }
    }
}
