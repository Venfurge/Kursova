using Kursova.Data;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace Kursova.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {
        protected INavigationService NavigationService { get; private set; }

        protected IItemsRepository ItemsRepository { get; set; }

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("repository", ItemsRepository);
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            ItemsRepository = parameters.GetValue<IItemsRepository>("repository");
        }

        public virtual void OnAppearing()
        {
            
        }

        public virtual void OnDisappearing()
        {
            
        }

        public virtual void Destroy()
        {

        }
    }
}
