using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Kursova.Standart;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using System.IO;

namespace Kursova.Droid
{
    [Activity(Label = "Kursova", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", 
        MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var dbPath = Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), "KursovaTest.db");
            var itemsRepository = new ItemsRepository(dbPath);

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer(), itemsRepository));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterPopupNavigationService();
        }
    }
}

