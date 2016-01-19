using Ninject;
using Ninject.Modules;

namespace WPDevToolkit.ViewModel
{
    public class ViewModelLocator : NinjectModule
    {
        public ViewModelLocator()
        {
            IocContainer.GetKernel().Load(this);
        }

        public override void Load()
        {
            Bind<IAboutViewModel>().To<AboutViewModel>();
        }

        public TViewModel GetViewModel<TViewModel>()
        {
            return IocContainer.GetKernel().Get<TViewModel>();
        }

        public IAboutViewModel AboutViewModel
        {
            get { return IocContainer.GetKernel().Get<IAboutViewModel>(); }
        }
    }
}
