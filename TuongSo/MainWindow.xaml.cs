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
using TuongSo.ViewControlers;

namespace TuongSo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVC Context => this.DataContext as MainWindowVC;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateFortune(object sender, RoutedEventArgs e)
        {
            if (Context.IsValid())
            {
                Context.CaculateResult();
                Context.ShowPyramid = true;
                this.pyramidView.SetBaseValue();
            }
            else
            {
                MessageBox.Show("Thong tin khong dung","Bao loi");
            }
        }
        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
