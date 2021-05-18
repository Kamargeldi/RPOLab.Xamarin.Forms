using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPOLab.FireBaseDB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RPOLab.Models;

namespace RPOLab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewFilmPage : ContentPage
    {
        Film _film;
        FireBaseService _service;
        string _imageUrl = null;
        string _videoUrl = null;
        public NewFilmPage()
        {
            InitializeComponent();
            _service = new FireBaseService();
            _film = new Film();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ratingList = _service.GetRatings();
            newFilmRatingPicker.ItemsSource = ratingList;
        }

        private async void createFilmButton_Clicked(object sender, EventArgs e)
        {
            _film.Name = newFilmNameEntry.Text;
            _film.Genre = newFilmGenreEntry.Text;
            _film.Description = newFilmDescEditor.Text;
            _film.Language = newFilmLanguageEntry.Text;
            _film.Producer = newFilmProducerEntry.Text;
            int temp;
            if (int.TryParse(newFilmYearEntry.Text, out temp))
                _film.Year = temp;
            if (int.TryParse(newFilmRatingPicker.SelectedItem != null ? newFilmRatingPicker.SelectedItem.ToString() : "", out temp))
                _film.Rating = temp;

            if (_imageUrl != null)
            {
                _film.ImageUrl = _imageUrl;
                _film.HasImage = true;
            }
            else
            {
                _film.ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
            }

            if (_videoUrl != null)
            {
                _film.VideoUrl = _videoUrl;
                _film.HasVideo = true;
            }
            

            await _service.AddFilm(_film);
            await Navigation.PopAsync();
        }

        private async void uploadImageButton_Clicked(object sender, EventArgs e)
        {
            var image = await _service.PickImage();
            _imageUrl = await _service.UploadImage(image);
        }

        private async void uploadVideoButton_Clicked(object sender, EventArgs e)
        {
            var video = await _service.PickVideo();
            _videoUrl = await _service.UploadVideo(video);
        }
    }
}