using RPOLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPOLab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        Film _film;
        public DetailsPage(Film film)
        {
            InitializeComponent();
            _film = film;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (_film != null && _film.HasVideo)
            {
                await Navigation.PushAsync(new VideoPage(_film));
            }
            else
            {
                await DisplayAlert("Message", "Film has not a video.", "OK");
            }
        }
    }
}