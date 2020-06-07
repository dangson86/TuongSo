using DomainContext;
using DomainContext.Entities;
using DomainContext.Interfaces;
using Microsoft.Practices.Unity;

namespace GeneralDI
{
    public class TuongSoServiceContainer
    {
        private static readonly IUnityContainer container = new UnityContainer();
        static TuongSoServiceContainer()
        {
            container.RegisterInstance(LocalDomainContext.GetContext());
            container.RegisterType<IAppState, AppState>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer GetCollection()
        {
            return container;
        }
    }
}
