using System.Windows;
using TuongSo.Navigators;

namespace TuongSo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Navigator Nav { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
        }        

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void root_Loaded(object sender, RoutedEventArgs e)
        {
            Nav = this.localNavigator;
        }
    }
}
