namespace DomainContext.Generics
{
    public interface IValueType<T>
    {
        T Model { get; set; }
    }
    public abstract class BaseViewModel : ObservableModel
    { }

   
    public abstract class BaseViewModel<T, K> : BaseViewModel, IValueType<T> where K : BaseViewModel<T, K>, IValueType<T>, new()
    {
        public T Model { get; set; }

        public static implicit operator BaseViewModel<T, K>(T input)
        {
            K vm = new K
            {
                Model = input
            };
            return vm;
        }
    }
}
