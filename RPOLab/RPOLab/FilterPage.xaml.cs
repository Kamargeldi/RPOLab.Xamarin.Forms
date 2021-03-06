using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPOLab.FireBaseDB;
using RPOLab.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPOLab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        FireBaseService _service = new FireBaseService();
        Filter _filter;
        public FilterPage(Filter filter)
        {
            InitializeComponent();
            _filter = filter;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var filters = _service.GetAvailableFilterValues();

            filterNamePicker.ItemsSource = filters.names;
            filterProducerPicker.ItemsSource = filters.producers;
            filterRatingPicker.ItemsSource = filters.ratings;
            filterYearPicker.ItemsSource = filters.years;
            

            filterHasImage.IsChecked = _filter.HasImage;
            filterHasVideo.IsChecked = _filter.HasVideo;
            filterNamePicker.SelectedIndex = filterNamePicker.Items.IndexOf(_filter.Name);
            filterProducerPicker.SelectedIndex = filterProducerPicker.Items.IndexOf(_filter.Producer);
            filterRatingPicker.SelectedIndex = filterRatingPicker.Items.IndexOf(_filter.Rating.ToString());
            filterYearPicker.SelectedIndex = filterYearPicker.Items.IndexOf(_filter.Year.ToString());

        }

        private async void applyFilterButton_Clicked(object sender, EventArgs e)
        {
            Filter filters = new Filter();

            filters.HasImage = filterHasImage.IsChecked;
            filters.HasVideo = filterHasVideo.IsChecked;
            filters.Name = filterNamePicker.SelectedIndex != -1 ? filterNamePicker.Items[filterNamePicker.SelectedIndex] : "";
            filters.Producer = filterProducerPicker.SelectedIndex != -1 ? filterProducerPicker.Items[filterProducerPicker.SelectedIndex] : "";
            filters.Rating = filterRatingPicker.SelectedIndex != -1 ? int.Parse(filterRatingPicker.Items[filterRatingPicker.SelectedIndex]) : 0;
            filters.Year = filterYearPicker.SelectedIndex != -1 ? int.Parse(filterYearPicker.Items[filterYearPicker.SelectedIndex]): 0;
            filters.Cleared = false;

            await _service.SaveFilters(filters);
            await Navigation.PopAsync();
        }

        private async void clearFilterButton_Clicked(object sender, EventArgs e)
        {
            Filter filters = new Filter();
            filters.HasImage = false;
            filters.HasVideo = false;
            filters.Name = null;
            filters.Producer = null;
            filters.Rating = 0;
            filters.Year = 0;
            filters.Cleared = true;

            await _service.SaveFilters(filters);
            await Navigation.PopAsync();
        }
    }
}