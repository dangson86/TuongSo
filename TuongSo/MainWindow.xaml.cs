using System.Windows;
using TuongSo.Navigators;

namespace TuongSo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Navigator.Current;
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void New_Py_Click(object sender, RoutedEventArgs e)
        {
            var localNavigator = Navigator.Current;
            localNavigator.AppState.SelectedCustomerId = null;
            localNavigator.NavigateToVM<ViewModels.PyCalVM>();
        }
    }
}
