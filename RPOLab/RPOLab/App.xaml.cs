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
            var settings = new FireBaseDB.FireBaseService().GetSettings();
            var lang = settings.Language;
            var darkMode = settings.DarkMode;
            if (darkMode)
            {
                RPOLab.Models.StaticData.DarkModeBackGroundColor = Color.Gray;
                RPOLab.Models.StaticData.DarkModeNavColor = Color.Gray;
            }
            else
            {
                RPOLab.Models.StaticData.DarkModeNavColor = Color.DarkTurquoise;
                RPOLab.Models.StaticData.DarkModeBackGroundColor = Color.White;
            }

            if (settings.FontName == "Serif Monospace")
            {
                RPOLab.Models.StaticData.FontName = "serif-monospace";
            } 
            else
            {
                RPOLab.Models.StaticData.FontName = settings.FontName;
            }

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
            mainPage.BarBackgroundColor = RPOLab.Models.StaticData.DarkModeNavColor; ;
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
