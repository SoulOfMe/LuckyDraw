﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LuckyDraw
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //MainWindow window = new LuckyDraw.MainWindow();
            //window.Show();

            AnimationWindow winodw = new AnimationWindow();
            winodw.Show();
        }
    }
}
