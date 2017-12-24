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
            _timer.Interval = new TimeSpan(2000);
            _timer.Tick += _timer_Tick;
        }

        private DoubleAnimation _doubleAnimation = new DoubleAnimation();
        private bool _isEntrieView = false; //全屏状态
        DispatcherTimer _timer = new DispatcherTimer(); //屏幕滚动定时器

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
                        SwitchList();
                        //_timer.Start();
                        return;
                    }
                    else
                    {
                        //_timer.Stop();

                        //_doubleAnimation.To = 0;
                        //if (prizelist1.Children.Count > 0)
                        //{
                        //    prizelist1.Children[0].BeginAnimation(Canvas.BottomProperty, _doubleAnimation);
                        //}
                        //for(int i=1; i < prizelist1.Children.Count; i++)
                        //{
                        //    prizelist1.Children.RemoveAt(i);
                        //}
                    }
                }
            }
            catch (Exception ex) { }
        }

        private bool _isshow = false;
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

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
            _doubleAnimation.DecelerationRatio = 0.5; //动画减速
            _doubleAnimation.FillBehavior = FillBehavior.HoldEnd; //设置动画完成后执行的操作
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            SwitchList();
        }

        private void LoadData()
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
                Canvas.SetTop(person1, i * 150);
                Canvas.SetTop(person2, i * 150);
                Canvas.SetTop(person3, i * 150);
                Canvas.SetTop(person4, i * 150);
                Canvas.SetTop(person5, i * 150);

                personlist1.Children.Add(person1);
                personlist2.Children.Add(person2);
                personlist3.Children.Add(person3);
                personlist4.Children.Add(person4);
                personlist5.Children.Add(person5);
            }
        }

        private void SwitchList()
        {
            int move = 1500;

            foreach (var i in prizelist1.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in prizelist2.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in prizelist3.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in prizelist4.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in prizelist5.Children)
            {
                var item = i as PrizeItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.BottomProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }

            //Person
            foreach (var i in personlist1.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.TopProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in personlist2.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.TopProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in personlist3.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.TopProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in personlist4.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.TopProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
            foreach (var i in personlist5.Children)
            {
                var item = i as PersonItem;
                if (item != null)
                {
                    double transBottom = Convert.ToDouble(item.GetValue(Canvas.TopProperty));
                    _doubleAnimation.To = move + transBottom;
                    item.BeginAnimation(Canvas.BottomProperty, _doubleAnimation); //设置动画应用的属性并启动动画 
                }
            }
        }
    }
}
