using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LuckyDraw
{
    /// <summary>
    /// AnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationWindow : Window
    {
        public AnimationWindow()
        {
            InitializeComponent();

            AnimationLoad();
            LoadData();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _timer.Tick += _timer_Tick;
        }

        private DoubleAnimation _doubleAnimation = new DoubleAnimation();
        private DoubleAnimation _getresultAnimaiton = new DoubleAnimation();
        private bool _isEntrieView = false; //全屏状态
        DispatcherTimer _timer = new DispatcherTimer(); //屏幕滚动定时器
        private MainViewModel _mainViewModel = new MainViewModel();
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
                    _isshow = !_isshow;
                    if (_isshow)
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

                        _timer.Start();
                    }
                    else
                    {
                        _timer.Stop();

                        GetDrawResult();
                    }
                }
            }
            catch (Exception ex) { }
        }

        private bool _isshow = false;

        private void EntireView()
        {
            if (!_isEntrieView)
            {
                //设置全屏
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;

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


                this.Left = 100;
                this.Top = 100;
                this.Width = 800;
                this.Height = 600;

                _isEntrieView = false;
            }
        }

        private void AnimationLoad()
        {
            _doubleAnimation.Duration = TimeSpan.FromSeconds(0.5); //设置动画时间线长度
            _doubleAnimation.AccelerationRatio = 0.1; //动画加速
            _doubleAnimation.DecelerationRatio = 0.1; //动画减速
            _doubleAnimation.FillBehavior = FillBehavior.HoldEnd; //设置动画完成后执行的操作

            _getresultAnimaiton.Duration = TimeSpan.FromSeconds(3);
            _getresultAnimaiton.AccelerationRatio = 0;
            _getresultAnimaiton.DecelerationRatio = 1;
            _getresultAnimaiton.FillBehavior = FillBehavior.HoldEnd;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            SwitchList();
        }

        private void LoadTestData()
        {
            for (int i = -10; i < 10; i++)
            {
                PrizeItem prize1 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150,
                };
                PrizeItem prize2 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                PrizeItem prize3 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                PrizeItem prize4 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                PrizeItem prize5 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                Canvas.SetBottom(prize1, i * 150);
                Canvas.SetBottom(prize2, i * 150);
                Canvas.SetBottom(prize3, i * 150);
                Canvas.SetBottom(prize4, i * 150);
                Canvas.SetBottom(prize5, i * 150);

                prizelist1.Children.Add(prize1);
                prizelist2.Children.Add(prize2);
                prizelist3.Children.Add(prize3);
                prizelist4.Children.Add(prize4);
                prizelist5.Children.Add(prize5);

                PersonItem person1 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                PersonItem person2 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                PersonItem person3 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                PersonItem person4 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                PersonItem person5 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                Canvas.SetBottom(person1, i * 150);
                Canvas.SetBottom(person2, i * 150);
                Canvas.SetBottom(person3, i * 150);
                Canvas.SetBottom(person4, i * 150);
                Canvas.SetBottom(person5, i * 150);

                personlist1.Children.Add(person1);
                personlist2.Children.Add(person2);
                personlist3.Children.Add(person3);
                personlist4.Children.Add(person4);
                personlist5.Children.Add(person5);
            }
        }

        private void SwitchList()
        {
            //移动距离
            int move = 150;
            int endposition = 600;

            //需要删除的对象列表
            List<UIElement> deletes = new List<UIElement>();

            //添加图片的位置
            double endDouble = 0;

            foreach (var i in prizelist1.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (transBottom > endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom < endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            //删除Item
            foreach (var i in deletes)
            {
                prizelist1.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PrizeItem prizeItem = new PrizeItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                prizelist1.Children.Add(prizeItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in prizelist2.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To > endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom < endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                prizelist2.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PrizeItem prizeItem = new PrizeItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                prizelist2.Children.Add(prizeItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in prizelist3.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To > endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom < endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                prizelist3.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PrizeItem prizeItem = new PrizeItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                prizelist3.Children.Add(prizeItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in prizelist4.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To > endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom < endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                prizelist4.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PrizeItem prizeItem = new PrizeItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                prizelist4.Children.Add(prizeItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in prizelist5.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To > endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom < endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                prizelist5.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PrizeItem prizeItem = new PrizeItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                prizelist5.Children.Add(prizeItem);
            }
            endDouble = 0;
            deletes.Clear();

            //Person
            foreach (var i in personlist1.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = transBottom - move;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To < -endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom > endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                personlist1.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PersonItem personItem = new PersonItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                Canvas.SetBottom(personItem, i * 150 + endDouble);
                personlist1.Children.Add(personItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in personlist2.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = transBottom - move;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To < -endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom > endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                personlist2.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PersonItem personItem = new PersonItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                Canvas.SetBottom(personItem, i * 150 + endDouble);
                personlist2.Children.Add(personItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in personlist3.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = transBottom - move;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To < -endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom > endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                personlist3.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PersonItem personItem = new PersonItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                Canvas.SetBottom(personItem, i * 150 + endDouble);
                personlist3.Children.Add(personItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in personlist4.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = transBottom - move;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To < -endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom > endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                personlist4.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PersonItem personItem = new PersonItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                Canvas.SetBottom(personItem, i * 150 + endDouble);
                personlist4.Children.Add(personItem);
            }
            endDouble = 0;
            deletes.Clear();

            foreach (var i in personlist5.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = transBottom - move;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                    transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    if (_doubleAnimation.To < -endposition)
                    {
                        deletes.Add(item);
                    }
                    if (transBottom > endDouble)
                    {
                        endDouble = transBottom;
                    }

                    //透明度
                    if (transBottom != 0)
                    {
                        item.Opacity = 1 / (Math.Abs(transBottom) / 150 + 1);
                    }
                    else
                    {
                        item.Opacity = 1;
                    }
                }
            }
            foreach (var i in deletes)
            {
                personlist5.Children.Remove(i);
            }
            //添加Item
            for (int i = 0; i < deletes.Count; i++)
            {
                PersonItem personItem = new PersonItem()
                {
                    Width = 150,
                    Height = 150
                };
                int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                Canvas.SetBottom(personItem, i * 150 + endDouble);
                personlist5.Children.Add(personItem);
            }
            endDouble = 0;
            deletes.Clear();
        }

        private void LoadData()
        {
            for (int i = -5; i < 6; i++)
            {
                PrizeItem prize1 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150,
                };
                int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prize1.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                PrizeItem prize2 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prize2.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                PrizeItem prize3 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prize3.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                PrizeItem prize4 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prize4.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                PrizeItem prize5 = new PrizeItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                prize5.prizename.Text = _mainViewModel.PrizesList[rad].Name;

                prize1.Opacity = 0.5;
                prize2.Opacity = 0.5;
                prize3.Opacity = 0.5;
                prize4.Opacity = 0.5;
                prize5.Opacity = 0.5;
                Canvas.SetBottom(prize1, i * 150);
                Canvas.SetBottom(prize2, i * 150);
                Canvas.SetBottom(prize3, i * 150);
                Canvas.SetBottom(prize4, i * 150);
                Canvas.SetBottom(prize5, i * 150);

                prizelist1.Children.Add(prize1);
                prizelist2.Children.Add(prize2);
                prizelist3.Children.Add(prize3);
                prizelist4.Children.Add(prize4);
                prizelist5.Children.Add(prize5);

                PersonItem person1 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                person1.departname.Text = _mainViewModel.PeopleList[rad].Department;
                person1.personname.Text = _mainViewModel.PeopleList[rad].Name;
                PersonItem person2 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                person2.departname.Text = _mainViewModel.PeopleList[rad].Department;
                person2.personname.Text = _mainViewModel.PeopleList[rad].Name;
                PersonItem person3 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                person3.departname.Text = _mainViewModel.PeopleList[rad].Department;
                person3.personname.Text = _mainViewModel.PeopleList[rad].Name;
                PersonItem person4 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                person4.departname.Text = _mainViewModel.PeopleList[rad].Department;
                person4.personname.Text = _mainViewModel.PeopleList[rad].Name;
                PersonItem person5 = new PersonItem()
                {
                    Height = 150,
                    Width = 150
                };
                rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                person5.departname.Text = _mainViewModel.PeopleList[rad].Department;
                person5.personname.Text = _mainViewModel.PeopleList[rad].Name;

                person1.Opacity = 0.5;
                person2.Opacity = 0.5;
                person3.Opacity = 0.5;
                person4.Opacity = 0.5;
                person5.Opacity = 0.5;
                Canvas.SetBottom(person1, i * 150);
                Canvas.SetBottom(person2, i * 150);
                Canvas.SetBottom(person3, i * 150);
                Canvas.SetBottom(person4, i * 150);
                Canvas.SetBottom(person5, i * 150);

                personlist1.Children.Add(person1);
                personlist2.Children.Add(person2);
                personlist3.Children.Add(person3);
                personlist4.Children.Add(person4);
                personlist5.Children.Add(person5);
            }
        }

        //得到抽奖结果
        private void GetDrawResult()
        {
            Dictionary<Prize, Person> drawList = new Dictionary<Prize, Person>();

            //图片最后的位置
            double endDouble = -450;

            int endposition = 500;

            //需要删除的对象列表
            List<UIElement> deletes = new List<UIElement>();

            var prize1 = _mainViewModel.RandomPrize;
            var person1 = _mainViewModel.RandomPerson;
            if (prize1 != null && person1 != null)
            {
                //找到最后一个Item
                foreach (var i in prizelist1.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom <= -450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom <= -300 && transBottom > -450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }

                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PrizeItem prizeItem = new PrizeItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        prizeItem.prizename.Text = prize1.Name;
                        prizeItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                        prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                        prizeItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                    prizelist1.Children.Add(prizeItem);
                }
                //转过去
                foreach (var i in prizelist1.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble + 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom > endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    prizelist1.Children.Remove(i);
                }
                deletes.Clear();

                endDouble = 450;
                //Person  找到最后一个Item
                foreach (var i in personlist1.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom >= 450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom >= 300 && transBottom < 450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }
                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PersonItem personItem = new PersonItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        personItem.departname.Text = person1.Department;
                        personItem.personname.Text = person1.Name;
                        personItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                        personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                        personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                        personItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(personItem, i * 150 + endDouble);
                    personlist1.Children.Add(personItem);
                }
                //转过去
                foreach (var i in personlist1.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble - 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom < -endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    personlist1.Children.Remove(i);
                }
                deletes.Clear();
                endDouble = 0;

                drawList.Add(prize1, person1);
            }
            else
            {
                foreach (var i in prizelist1.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }

                foreach (var i in personlist1.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }
            }

            var prize2 = _mainViewModel.RandomPrize;
            var person2 = _mainViewModel.RandomPerson;
            if (prize2 != null && person2 != null)
            {
                endDouble = -450;
                //找到最后一个Item
                foreach (var i in prizelist2.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom <= -450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom <= -300 && transBottom > -450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }

                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PrizeItem prizeItem = new PrizeItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        prizeItem.prizename.Text = prize2.Name;
                        prizeItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                        prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                        prizeItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                    prizelist2.Children.Add(prizeItem);
                }
                //转过去
                foreach (var i in prizelist2.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble + 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom > endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    prizelist2.Children.Remove(i);
                }
                deletes.Clear();

                endDouble = 450;
                //Person  找到最后一个Item
                foreach (var i in personlist2.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom >= 450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom >= 300 && transBottom < 450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }
                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PersonItem personItem = new PersonItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        personItem.departname.Text = person2.Department;
                        personItem.personname.Text = person2.Name;
                        personItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                        personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                        personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                        personItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(personItem, i * 150 + endDouble);
                    personlist2.Children.Add(personItem);
                }
                //转过去
                foreach (var i in personlist2.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble - 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom < -endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    personlist2.Children.Remove(i);
                }
                deletes.Clear();
                endDouble = 0;

                drawList.Add(prize2, person2);
            }
            else
            {
                foreach (var i in prizelist2.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }

                foreach (var i in personlist2.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }
            }

            var prize3 = _mainViewModel.RandomPrize;
            var person3 = _mainViewModel.RandomPerson;
            if (prize3 != null && person3 != null)
            {
                endDouble = -450;
                //找到最后一个Item
                foreach (var i in prizelist3.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom <= -450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom <= -300 && transBottom > -450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }

                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PrizeItem prizeItem = new PrizeItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        prizeItem.prizename.Text = prize3.Name;
                        prizeItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                        prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                        prizeItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                    prizelist3.Children.Add(prizeItem);
                }
                //转过去
                foreach (var i in prizelist3.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble + 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom > endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    prizelist3.Children.Remove(i);
                }
                deletes.Clear();

                endDouble = 450;
                //Person  找到最后一个Item
                foreach (var i in personlist3.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom >= 450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom >= 300 && transBottom < 450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }
                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PersonItem personItem = new PersonItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        personItem.departname.Text = person3.Department;
                        personItem.personname.Text = person3.Name;
                        personItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                        personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                        personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                        personItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(personItem, i * 150 + endDouble);
                    personlist3.Children.Add(personItem);
                }
                //转过去
                foreach (var i in personlist3.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble - 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom < -endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    personlist3.Children.Remove(i);
                }
                deletes.Clear();
                endDouble = 0;

                drawList.Add(prize3, person3);
            }
            else
            {
                foreach (var i in prizelist3.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }

                foreach (var i in personlist3.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }
            }

            var prize4 = _mainViewModel.RandomPrize;
            var person4 = _mainViewModel.RandomPerson;
            if (prize4 != null && person4 != null)
            {
                endDouble = -450;
                //找到最后一个Item
                foreach (var i in prizelist4.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom <= -450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom <= -300 && transBottom > -450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }

                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PrizeItem prizeItem = new PrizeItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        prizeItem.prizename.Text = prize4.Name;
                        prizeItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                        prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                        prizeItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                    prizelist4.Children.Add(prizeItem);
                }
                //转过去
                foreach (var i in prizelist4.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble + 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom > endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    prizelist4.Children.Remove(i);
                }
                deletes.Clear();

                endDouble = 450;
                //Person  找到最后一个Item
                foreach (var i in personlist4.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom >= 450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom >= 300 && transBottom < 450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }
                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PersonItem personItem = new PersonItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        personItem.departname.Text = person4.Department;
                        personItem.personname.Text = person4.Name;
                        personItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                        personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                        personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                        personItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(personItem, i * 150 + endDouble);
                    personlist4.Children.Add(personItem);
                }
                //转过去
                foreach (var i in personlist4.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble - 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom < -endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    personlist4.Children.Remove(i);
                }
                deletes.Clear();
                endDouble = 0;

                drawList.Add(prize4, person4);
            }
            else
            {
                foreach (var i in prizelist4.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }

                foreach (var i in personlist4.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }
            }

            var prize5 = _mainViewModel.RandomPrize;
            var person5 = _mainViewModel.RandomPerson;
            if (prize5 != null && person5 != null)
            {
                endDouble = -450;
                //找到最后一个Item
                foreach (var i in prizelist5.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom <= -450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom <= -300 && transBottom > -450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }

                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PrizeItem prizeItem = new PrizeItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        prizeItem.prizename.Text = prize5.Name;
                        prizeItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PrizesList.Count - 1);
                        prizeItem.prizename.Text = _mainViewModel.PrizesList[rad].Name;
                        prizeItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(prizeItem, -i * 150 + endDouble);
                    prizelist5.Children.Add(prizeItem);
                }
                //转过去
                foreach (var i in prizelist5.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble + 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom > endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    prizelist5.Children.Remove(i);
                }
                deletes.Clear();

                endDouble = 450;
                //Person  找到最后一个Item
                foreach (var i in personlist5.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom >= 450)
                        {
                            deletes.Add(item);
                        }
                        if (transBottom >= 300 && transBottom < 450)
                        {
                            endDouble = transBottom;
                        }
                        item.Opacity = 0.5;
                    }
                }
                //加5个Item,继续转
                for (int i = 1; i < 6; i++)
                {
                    PersonItem personItem = new PersonItem()
                    {
                        Width = 150,
                        Height = 150
                    };
                    if (i == 1)
                    {
                        personItem.departname.Text = person5.Department;
                        personItem.personname.Text = person5.Name;
                        personItem.Opacity = 1;
                    }
                    else
                    {
                        int rad = _random.Next(_mainViewModel.PeopleList.Count - 1);
                        personItem.departname.Text = _mainViewModel.PeopleList[rad].Department;
                        personItem.personname.Text = _mainViewModel.PeopleList[rad].Name;
                        personItem.Opacity = 0.5;
                    }
                    Canvas.SetBottom(personItem, i * 150 + endDouble);
                    personlist5.Children.Add(personItem);
                }
                //转过去
                foreach (var i in personlist5.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        _getresultAnimaiton.To = -endDouble - 150 + transBottom;
                        item.BeginAnimation(Canvas.BottomProperty, _getresultAnimaiton); //设置动画应用的属性并启动动画 
                        transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                        if (transBottom < -endposition)
                        {
                            deletes.Add(item);
                        }
                    }
                }
                foreach (var i in deletes)
                {
                    personlist5.Children.Remove(i);
                }
                deletes.Clear();
                endDouble = 0;

                drawList.Add(prize5, person5);
            }
            else
            {
                foreach (var i in prizelist5.Children)
                {
                    var item = i as PrizeItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }

                foreach (var i in personlist5.Children)
                {
                    var item = i as PersonItem;
                    if (item != null)
                    {
                        item.Opacity = 0.5;
                    }
                }
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
