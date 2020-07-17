using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartLED
{
    public partial class App : Application
    {
        public static readonly NavigationPage Navigation = new NavigationPage();

        public App()
        {
            InitializeComponent();
            MainPage = Navigation;

            Navigation.PushAsync(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
