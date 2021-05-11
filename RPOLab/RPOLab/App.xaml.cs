using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

namespace RPOLab
{
    public partial class App : Application
    {
        public App()
        {
            var lang = new FireBaseDB.FireBaseService().GetSettings().Language;
            if (lang == "English")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            }

            InitializeComponent();

            var mainPage = new NavigationPage(new MainPage());
            mainPage.BarBackgroundColor = Color.DarkTurquoise;
            mainPage.BarTextColor = Color.White;
            MainPage = mainPage;
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
