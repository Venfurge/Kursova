using Prism;
using Prism.Ioc;
using Kursova.ViewModels;
using Kursova.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kursova.Data;
using Prism.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Kursova
{
    public partial class App
    {
        public App() : this(null, null) { }

        public App(IPlatformInitializer initializer, IItemsRepository itemsRepository) 
            : base(initializer)
        {
            var parameters = new NavigationParameters();
            parameters.Add("repository", itemsRepository);
            Navigate(parameters);
        }

        protected async void Navigate(INavigationParameters parameters)
        {
            await NavigationService.NavigateAsync("NavigationPage/MainPage", parameters);
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivitiesPage, ActivitiesPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityCreationPopupPage, ActivityCreationPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectedActivityPopupPage, SelectedActivityPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityCompletingPopupPage, ActivityCompletingPopupPageViewModel>();
        }
    }
}
