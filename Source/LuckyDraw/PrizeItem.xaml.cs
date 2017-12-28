using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuckyDraw
{
    /// <summary>
    /// PrizeItem.xaml 的交互逻辑
    /// </summary>
    public partial class PrizeItem : UserControl
    {
        public PrizeItem()
        {
            InitializeComponent();
        }

        public void SetBackground(string name)
        {
            try
            {
                string path = @"pack://application:,,,/PrizeImage/" +
                      name + ".png";

                BitmapImage image = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = image;

                this.Background = brush;
            }
            catch (Exception e)
            {

            }
        }
    }
}
