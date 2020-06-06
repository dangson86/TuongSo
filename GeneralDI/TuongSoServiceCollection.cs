using DomainContext;
using Microsoft.Practices.Unity;

namespace GeneralDI
{
    public class TuongSoServiceCollection
    {
        private static readonly IUnityContainer container = new UnityContainer();
        static TuongSoServiceCollection()
        {
            container.RegisterInstance(LocalDomainContext.GetContext());
        }

        public static IUnityContainer GetCollection()
        {
            return container;
        }
    }
}
