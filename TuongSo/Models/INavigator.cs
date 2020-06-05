using System.Windows.Input;
using TuongSo.ViewModels;

namespace TuongSo.Models
{
    public interface INavigator
    {
        BaseVC CurrentViewModel { get; }
        ICommand UpdateViewModel { get; }
    }
}
