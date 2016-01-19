using Ninject;
using WPDevToolkit.Services;

namespace WPDevToolkit
{
    public abstract class IocContainer
    {
        private static IKernel _kernel;

        public static IKernel GetKernel()
        {
            if (_kernel != null)
            {
                _kernel = new StandardKernel();
                RegisterIoCBindings(_kernel);
            }
            return _kernel;
        }

        protected static void RegisterIoCBindings(IKernel kernel)
        {
            kernel.Bind<IConstantsService>().To<ConstantsBase>();
            // TODO same for all services
        }
    }
}
