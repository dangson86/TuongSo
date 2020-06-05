using DomainContext;
using System.Windows;

namespace TuongSo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await LocalDomainContext.GetContext();
        }
    }
}
