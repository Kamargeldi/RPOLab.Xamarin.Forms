using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPOLab.FireBaseDB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RPOLab.Models;
using System.Threading;

namespace RPOLab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        FireBaseService _service = new FireBaseService();
        Settings _settings;
        public SettingsPage(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            fontNamePicker.ItemsSource = _service.GetFontNames();
            languagePicker.ItemsSource = _service.GetLanguages();

            fontNamePicker.SelectedIndex = fontNamePicker.Items.IndexOf(_settings.FontName);
            languagePicker.SelectedIndex = languagePicker.Items.IndexOf(_settings.Language);
            darkModeCheker.IsChecked = _settings.DarkMode;
        }

        private async void saveSettingsButton_Clicked(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.DarkMode = darkModeCheker.IsChecked;
            settings.FontName = fontNamePicker.Items[fontNamePicker.SelectedIndex];
            settings.Language = languagePicker.Items[languagePicker.SelectedIndex];
            await _service.SaveSettings(settings);
            await Navigation.PopAsync();
        }
    }
}